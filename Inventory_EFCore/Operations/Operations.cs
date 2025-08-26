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
using Dapper;
using System.Collections.Immutable;

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

                // include null
                var output4 = dbcontext.Categories
                            .Join(dbcontext.Products,
                                c => c.CategoryId,
                                p => p.CategoryId,
                                (c, p) => new { c, p })
                            .GroupJoin(dbcontext.ProductReviews,
                                cp => cp.p.ProductId,
                                pr => pr.ProductId,
                                (cp, prs) => new { cp, prs })
                            .SelectMany(
                                x => x.prs.DefaultIfEmpty(), 
                                (x, pr) => new
                                {
                                    CategoryName = x.cp.c.CategoryName,
                                    ProductName = x.cp.p.Name,
                                    Rating = pr != null ? pr.Rating : 0
                                });

                foreach (var item in output4)
                {
                    Console.WriteLine($"{item.CategoryName} | {item.ProductName} | {item.Rating}");
                }

                //var output4 = from c in dbcontext.Categories
                //              join p in dbcontext.Products
                //                  on c.CategoryId equals p.CategoryId
                //              join pr in dbcontext.ProductReviews
                //                  on p.ProductId equals pr.ProductId into gj
                //              from pr in gj.DefaultIfEmpty()   // LEFT JOIN
                //              select new
                //              {
                //                  CategoryName = c.CategoryName,
                //                  ProductName = p.Name,
                //                  Rating = pr != null ? pr.Rating : 0
                //              };
            }
        }

        public void AddReviews(AddReview input)
        {
            var reviews = new ProductReview
            {
                CustomerId = input.CustomerID,
                ProductId = input.productID,
                Rating = input.Rating,
                Comments = input.Commands,
                ReviewDate = input.Date
            };

            using(var dbcontext = new InventoryContext())
            {
                dbcontext.Add(reviews);
                dbcontext.SaveChanges();
            }
        }

        public void BulkInsertProducts(List<NewProductBulk> input)
        {
            using (var dbcontext = new InventoryContext())
            {
                var products = input.Select(x => new Product
                {
                    Name = x.ProductName,
                    CategoryId = x.CategoryId,
                    Price = x.Price
                }).ToList();

                dbcontext.Products.AddRange(products);
                dbcontext.SaveChanges();
            }
        }

        public void UpdatePrize(int ID)
        {
            using(var dbcontext = new InventoryContext())
            {
                var updateProduct = dbcontext.Products.Where(u => u.CategoryId == ID).ToList();

                foreach (var item in updateProduct)
                {
                    int up = (int)(item.Price * 0.10m);
                    item.Price += up;
                }

                dbcontext.SaveChanges();
            }
        }

        public void SaleProductus(List<SaleProductList> saleList, int customerID, int userID)
        {
            decimal totalAmount = 0;
            foreach (var item in saleList)
            {
                totalAmount += item.Price;
            }

            var saleTotal = new Sale
            {
                CustomerId = customerID,
                SaleDate = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = totalAmount,
                UserId = userID
            };

            using(var dbcontext = new InventoryContext())
            {
                dbcontext.Add(saleTotal);
                dbcontext.SaveChanges();

                int saleID = saleTotal.SaleId;

                var Productdetials = saleList.Select(e => new SalesDetail
                {
                    SaleId = saleID,
                    ProductId = e.ProductId,
                    Quantity = e.Quantity,
                    Price = e.Price,
                });

                dbcontext.AddRange(Productdetials);
                //dbcontext.
                dbcontext.SaveChanges();
            }
        }

        public void SupplierDetials(int Id)
        {
            using(var dbcontext = new InventoryContext())
            {
                var connection = dbcontext.Database.GetDbConnection();

                connection.Open();

                string sql = @"Select Name from Suppliers where supplier_id = @id";
                
                var result = connection.Query<SupplierName>(sql, new {id = Id}).ToList();

                foreach (var item in result)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        public void PurchaseDetails()
        {
            using (var dbcontext = new InventoryContext())
            {
                var output = dbcontext.PurchaseDetialsViews
                    .FromSqlRaw(@"SELECT sale_id, customer_name, sold_by, product_name, total_price  
                      FROM vw_SalesDetails")
                    .ToList();

                foreach (var item in output)
                {
                    Console.WriteLine($"{item.sale_id} {item.custome_name} {item.sold_by} {item.product_name} {item.total_price}");
                }
            }

        }
    }
}

