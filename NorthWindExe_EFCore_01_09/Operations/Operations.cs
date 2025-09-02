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
        InputValidations input = new InputValidations();
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
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine(item.ID + " - " + item.Name);
                        Console.WriteLine("---------------------------------------------");
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
            try
            {
                using (var dbcontext = new NorthWindContext())
                {
                    var customers = await dbcontext.Customers
                        .Where(c => c.Orders.Count > 1) 
                        .ToListAsync();

                    foreach (var customer in customers)
                    {
                        var sortedOrders = customer.Orders
                            .OrderBy(o => o.OrderDate.Value)
                            .ToList();

                        //if (sortedOrders.Count < 2)
                        //    continue;

                        double totalGap = 0;

                        for (int i = 1; i < sortedOrders.Count; i++)
                        {
                            totalGap += (sortedOrders[i].OrderDate.Value - sortedOrders[i - 1].OrderDate.Value).TotalDays;
                        }

                        double avgGap = totalGap / (sortedOrders.Count - 1);

                        Console.WriteLine($"{customer.CustomerId} | {customer.CompanyName} | {avgGap}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                                .Where(o => o.OrderDate.Value.Year == 1998)
                                    .SelectMany(o => o.OrderDetails)
                                    .Sum(od => (od.Quantity * od.UnitPrice) * (1 - (decimal)od.Discount))
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
                using (var dbcontext = new NorthWindContext())
                {
                    var customers = await dbcontext.Customers
                            .Where(c => c.Orders.Count >= 2)
                            .Include(c => c.Orders)
                            .ToListAsync();

                    string customerId = null;
                    string companyName = null;
                    double maxGapDays = 0;

                    foreach (var customer in customers)
                    {
                        var sorted = customer.Orders
                            .OrderBy(o => o.OrderDate.Value)
                            .ToList();

                        for (int i = 1; i < sorted.Count; i++)
                        {
                            double gap = (sorted[i].OrderDate.Value - sorted[i - 1].OrderDate.Value).TotalDays;
                            if (gap > maxGapDays)
                            {
                                maxGapDays = gap;
                                customerId = customer.CustomerId;
                                companyName = customer.CompanyName;
                            }
                        }
                    }

                    if(customerId != null )
                    {
                        Console.WriteLine(customerId+" - "+companyName+" - "+maxGapDays);
                    }
                }
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
                    var orders = await dbcontext.Orders
                        .Where(o => o.Customer.Country == "Germany"
                                    && o.OrderDate.HasValue
                                    && o.OrderDate.Value.Year == 1997)
                        .SelectMany(o => o.OrderDetails)
                        .ToListAsync();

                    

                    foreach (var order in orders)
                    {
                        order.Discount = 0.1f;
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
                    var ordersToDelete = await dbcontext.Orders
                        .Where(o => o.ShippedDate == null)
                        .ToListAsync();

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

        public void InsertNewOrder()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            string check = "";
            do
            {
                Console.WriteLine("Product List : ");
                int ProductID;
                decimal UnitPrize;
                using (var dbcontext = new NorthWindContext())
                {
                    var Plist = dbcontext.Products.ToList();

                CHECK_ProductID:
                    foreach (var product in Plist)
                    {
                        Console.WriteLine(product.ProductId + " : " + product.ProductName);
                    }

                    Console.Write("Enter the Product ID : ");
                    ProductID = input.IntCheck(Console.ReadLine());

                    if (!dbcontext.Products.Any(s => s.ProductId == ProductID))
                    {
                        Console.WriteLine("Product Id Not found, try again");
                        goto CHECK_ProductID;
                    }

                    var product2 = dbcontext.Products.Find(ProductID);


                    UnitPrize = (decimal)product2.UnitPrice;

                }

                Console.Write("Enter the Quantity : ");
                int Quantity = input.IntCheck(Console.ReadLine());

                var list = new OrderDetail
                {
                    ProductId = ProductID,
                    Quantity = (short)Quantity,
                    UnitPrice = UnitPrize,
                    Discount = 0
                };

                orderDetails.Add(list);

                Console.Write("Do you want add another product (yes/no) : ");
                check = Console.ReadLine().ToLower().Trim();
            } while (check == "yes");

            
        RECHECK_customerID:
            Console.Write("Enter the Customer ID : ");
            string CustomerID = input.CustomerIDCheck(Console.ReadLine());

            using (var dbcontext = new NorthWindContext())
            {
                if (!dbcontext.Customers.Any(a => a.CustomerId == CustomerID))
                {
                    Console.WriteLine("CustomerID not found");
                    goto RECHECK_customerID;
                }
            }

                Console.WriteLine("Employee List : ");
            int EmployeeID;
            using (var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    ID = s.EmployeeId,
                    Name = s.FirstName + " " + s.LastName
                }).ToList();

            RECHECK_empID:
                foreach (var item in list)
                {
                    Console.WriteLine(item.ID + " : " + item.Name);
                }

                Console.Write("Enter the Employee ID : ");
                EmployeeID = input.IntCheck(Console.ReadLine());

                if(!dbcontext.Employees.Any(s=> s.EmployeeId == EmployeeID))
                {
                    Console.WriteLine("Employee ID not Found");
                    goto RECHECK_empID;
                }
            }

            DateTime OrderDate = DateTime.Now;
            DateTime RequidDate = DateTime.Now; 
            DateTime ShippedDate = DateTime.Now.AddDays(3);

            int ShipID;
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Shippers.ToList();

            RECHECK_shipID:
                foreach (var item in list)
                {
                    Console.WriteLine(item.ShipperId + " : " + item.CompanyName);
                }

                Console.Write("Enter the Shipper ID : ");
                ShipID = input.IntCheck(Console.ReadLine());

                if(!dbcontext.Shippers.Any(a=> a.ShipperId == ShipID))
                {
                    Console.WriteLine("ShipperID not Found : ");
                    goto RECHECK_shipID;
                }
            }

            Console.Write("Enter the Fright : ");
            decimal Fright = input.DecimalCheck(Console.ReadLine());

            Console.Write("Enter the Ship Name : ");
            string ShipName = input.ShipNameCheck(Console.ReadLine());

            Console.Write("Enter the Address : ");
            string Address = input.AddressCheck(Console.ReadLine());

            Console.Write("Enter the Ship City : ");
            string ShipCity = input.CityCheck(Console.ReadLine());

            Console.Write("Enter the Ship County : ");
            string ShipCountry = input.CountryCheck(Console.ReadLine());

            var OrderMain = new Order
            {
                CustomerId = CustomerID,
                EmployeeId = EmployeeID,
                OrderDate = OrderDate,
                RequiredDate = RequidDate,
                ShippedDate = ShippedDate,
                ShipVia = ShipID,
                Freight = Fright,
                ShipAddress = Address,
                ShipName = ShipName,
                ShipCity = ShipCity,
                ShipCountry = ShipCountry
            };

            
            using (var dbcontext = new NorthWindContext())
            {
                dbcontext.Orders.Add(OrderMain);
                dbcontext.SaveChanges();

                int OrderID = OrderMain.OrderId;
         
                List<OrderDetail> orderDetailsnewone = new List<OrderDetail>();

                foreach (var item in orderDetails)
                {
                    var list = new OrderDetail
                    {
                        OrderId = OrderID,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        Discount = item.Discount
                    };

                    orderDetailsnewone.Add(list);
                }

                dbcontext.OrderDetails.AddRange(orderDetailsnewone);
                dbcontext.SaveChanges();
                Console.WriteLine($"Order Placed successfully- your Order id is {OrderID}");
                Console.WriteLine();
            }


        }
    }
}
