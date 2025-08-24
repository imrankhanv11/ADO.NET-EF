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

        public void ProductWithCat()
        {
            using(var dbcontext = new InventoryContext())
            {
                var output1 = dbcontext.Categories.ToList();

                foreach (var item in output1)
                {
                    Console.WriteLine(item.CategoryName);

                    foreach (var item1 in item.Products)
                    {
                        Console.WriteLine(item1.Name);
                    }
                    Console.WriteLine();
                }

                // Join (inner)
                var output = dbcontext.Categories.Join(dbcontext.Products,
                    c=> c.CategoryId,
                    p=> p.CategoryId,
                    (c,p)=> new
                    {
                        categoryName = c.CategoryName,
                        productName = p.Name
                    }
                    );

                foreach (var item in output)
                {
                    Console.WriteLine(item.categoryName + " : " + item.productName);
                }

                // join (inner)
                var output3 = from c in dbcontext.Categories
                             join p in dbcontext.Products
                             on c.CategoryId equals p.CategoryId
                             join pr in dbcontext.ProductReviews
                             on p.ProductId equals pr.ProductId
                             select new {
                                CategoryName = c.CategoryName,
                                ProductName = p.Name,
                                ProductReviews = pr.Rating
                };

                foreach (var item in output3)
                {
                    Console.WriteLine(item.CategoryName + " " + item.ProductName + " " + item.ProductReviews);
                }
            }
        }
    }
}

