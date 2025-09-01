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
    }
}
