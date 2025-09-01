using EXC_NorthWind_01_09_2025.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXC_NorthWind_01_09_2025
{
    public class Operations
    {
        public async Task EmpTerteroy()
        {
            try
            {
                using (var dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Employees.Select(s => new
                    {
                        ID = s.EmployeeId,
                        Name = s.FirstName + " " + s.LastName,
                        Territory = s.Territories.Select(t => t.TerritoryDescription)
                    }).ToListAsync();

                    foreach (var item in list)
                    {
                        Console.WriteLine(item.ID + " - " + item.Name);
                        foreach (var item2 in item.Territory)
                        {
                            Console.WriteLine(item2);
                        }
                        Console.WriteLine();
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task OrderWithoutOrderDetail()
        {
            try
            {
                using (var dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Orders
                        .Where(o => !o.OrderDetails.Any())
                        .Select(o => new
                        {
                            o.OrderId,
                            o.CustomerId,
                            o.OrderDate
                        })
                        .ToListAsync();

                    foreach (var order in list)
                    {
                        Console.WriteLine($"{order.OrderId} | {order.CustomerId} | {order.OrderDate}");
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void CatWithHightProductQuntity()
        {
            try
            {

                using (var dbcontext = new NorthWindContext())
                {
                    var result = dbcontext.Categories
                        .Select(c => new
                        {
                            CategoryName = c.CategoryName,
                            TotalQuantityOrdered = c.Products
                                .SelectMany(p => p.OrderDetails)
                                .Sum(od => (int?)od.Quantity) ?? 0
                        })
                        .OrderByDescending(x => x.TotalQuantityOrdered)
                        .FirstOrDefault();

                    Console.WriteLine($"{result.CategoryName} | {result.TotalQuantityOrdered}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CusAndTheirAvgOrders()
        {
            using(var dbcontext = new NorthWindContext())
            {
                //var list = dbcontext.Customers.Select(s=> new
                //{
                //    ID = s.CustomerId,
                //    Name = s.CompanyName,
                //    Avg = s.Orders.
                //})
            }
        }

        public async Task ShippersWithTotalOrders()
        {
            try
            {
                using(var dbcontext = new NorthWindContext())
                {
                    var result = await dbcontext.Shippers
                            .Select(s => new
                            {
                                ShipperName = s.CompanyName,
                                TotalRevenueHandled = s.Orders.Sum(o =>
                                    o.Freight +
                                    o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))
                                )
                            })
                            .ToListAsync();

                    foreach (var item in result)
                    {
                        Console.WriteLine(item.ShipperName + " --> " + item.TotalRevenueHandled);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ProductNotInOrders()
        {
            try
            {
                using(var dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Products.Where(s => !s.OrderDetails.Any()).ToListAsync();

                    foreach (var item in list)
                    {
                        Console.WriteLine($"{item.ProductId,-4} {item.ProductName, -20} {item.Category.CategoryName}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task RankEmp1998()
        {
            try
            {
                using( var dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Employees
                            .Select(e => new
                            {
                                EmployeeName = e.FirstName + " " + e.LastName,
                                TotalSales = e.Orders
                                .Where(o => o.OrderDate >= new DateTime(1998, 1, 1)
                                         && o.OrderDate < new DateTime(1999, 1, 1))
                                    .SelectMany(o => o.OrderDetails)
                                    .Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount))
                            })
                            .OrderByDescending(e => e.TotalSales)
                            .ToListAsync();

                    int count = 1;

                    foreach (var item in list)
                    {
                        Console.WriteLine($"Name : {item.EmployeeName,-17} TotalSale : {item.TotalSales, -5} Rank : {count++}");
                    }
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Report1997Sale()
        {
            try
            {
                using(var  dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Orders
                            .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997) 
                            .SelectMany(o => o.OrderDetails.Select(od => new
                            {
                                Month = o.OrderDate.Value.Month,
                                Sales = od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount)
                            }))
                            .GroupBy(x => x.Month)
                            .Select(g => new
                            {
                                Month = g.Key,
                                TotalSales = g.Sum(x => x.Sales)
                            })
                            .OrderBy(x => x.Month)
                            .ToListAsync();

                    foreach (var item in list)
                    {
                        Console.WriteLine(item.Month+" : "+item.TotalSales);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CusLongGap()
        {
            try
            {
                //using( var dbcontext = new NorthWindContext())
                //{
                //    var list = dbcontext.Customers.Select(s=> new
                //    {
                //        ID= s.CustomerId,
                //        Name = s.CompanyName,

                //    })
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CompareUnits()
        {
            try
            {
                using(var dbcontext = new NorthWindContext())
                {
                    var list = await dbcontext.Products.Where(s => s.UnitsInStock + s.UnitsOnOrder < s.ReorderLevel).ToListAsync();

                    foreach (var item in list)
                    {
                        Console.WriteLine($"ID : {item.ProductId,-3} Name : {item.ProductName,-18} Unit In Stock : {item.UnitsInStock,-3} Unints on Orders : {item.UnitsOnOrder,-3} ReorderLevel : {item.ReorderLevel} ");
                    }
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task top3Products()
        {
            try
            {
                //using(var dbcontext = new NorthWindContext())
                //{
                //    var list = dbcontext.
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task discount()
        {
            try
            {
                using (var dbcontext = new NorthWindContext())
                {
                    var orders = dbcontext.Orders
                        .Where(o => o.Customer.Country == "Germany"
                                    && o.OrderDate.HasValue
                                    && o.OrderDate.Value.Year == 1997)
                        .SelectMany(o => o.OrderDetails)
                        .ToList();

                    foreach (var order in orders)
                    {
                        foreach (var detail in order.Order.OrderDetails)
                        {
                            detail.Discount = 0.1f;
                        }
                    }

                    dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteOrders()
        {
            try
            {
                using (var dbcontext = new NorthWindContext())
                {
                    var ordersToDelete = dbcontext.Orders
                        .Where(o => o.ShippedDate == null)
                        .ToList();

                    foreach (var order in ordersToDelete)
                    {
                        var details = dbcontext.OrderDetails
                            .Where(d => d.OrderId == order.OrderId)
                            .ToList();

                        dbcontext.OrderDetails.RemoveRange(details);
                    }

                    dbcontext.Orders.RemoveRange(ordersToDelete);

                    dbcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
