using EFCore_DBFirstApp.DTO;
using EFCore_DBFirstApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EFCore_DBFirstApp
{
    public class Operations
    {
        public void AddProductsWareHouse(AddProductsinWareHouse input)
        {
            using (var dbcontext = new InventoryContext())
            {
                using (var transaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        var existingStock = dbcontext.ProductWarehouseStocks
                            .FirstOrDefault(pw => pw.ProductId == input.ProductID
                                               && pw.WarehouseId == input.WareHouseID);

                        if (existingStock != null)
                        {
                            existingStock.QuantityInStock += input.Quantity;
             
                        }
                        else
                        {
                            var warehouse = new ProductWarehouseStock
                            {
                                ProductId = input.ProductID,
                                WarehouseId = input.WareHouseID,
                                QuantityInStock = input.Quantity
                            };

                            dbcontext.ProductWarehouseStocks.Add(warehouse);
                        }

                        var log = new InventoryLog
                        {
                            ProductId = input.ProductID,
                            Action = "IN",
                            Quantity = input.Quantity,
                            ActionDate = DateOnly.FromDateTime(DateTime.Now),
                            UserId = input.UserId,
                            WarehouseId = input.WareHouseID
                        };

                        dbcontext.InventoryLogs.Add(log);

                        dbcontext.SaveChanges();
                        transaction.Commit();

                        Console.WriteLine("Successfully Added");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }

        public void CheckProuductStock(int productID)
        {
            var productid = new SqlParameter("@productID", productID);
            using (var dbcontext = new InventoryContext())
            {
                var value = dbcontext.CheckProductStocks.FromSqlRaw("Exec sp_CheckStock @productID", productid);

                foreach (var item in value)
                {
                    Console.WriteLine($"Prouduct Name : {item.productname}, Quantity : {item.totalStock}");
                }
            }
        }
    }
}

