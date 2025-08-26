using Excersice_EFCore.DTO;
using Excersice_EFCore.Models;
using Excersice_EFCore.Validations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Excersice_EFCore
{
    public class InputGetters
    {
        InputValidations input = new InputValidations();
        public void allProducts()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Select(p => new { p.Name, p.ProductNumber, p.ListPrice }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"Product Name : {item.Name}, ProductNumber : {item.ProductNumber}, ListPrice : {item.ListPrice}");
                }
            }
        }

        public void gelallEmp()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Employees.Where(e => e.HireDate > new DateOnly(2010, 12, 31)).Select(e=> new
                {
                    Date = e.HireDate,
                    FirstName = e.BusinessEntity.FirstName, 
                    LastName = e.BusinessEntity.LastName
                });

                foreach (var item in list)
                {
                    Console.WriteLine($"Full Name : {item.FirstName,-10} {item.LastName,-10} | Hire Date : {item.Date}");

                }

                //var list = dbcontext.Employees
                //        .Join(dbcontext.BusinessEntities,
                //            e => e.BusinessEntityId,
                //            b => b.BusinessEntityId,
                //            (e, b) => new
                //            {
                //                e.HireDate,
                //                b.Person.FirstName, b.Person.LastName,
                //            }).Where(e=> e.HireDate > new DateOnly(2010, 12, 31))
                //        .ToList();

                //foreach (var item in list)
                //{
                //    Console.WriteLine(item.FirstName + " " + item.LastName + " - " + item.HireDate);
                //}
            }
        }

        public void Top10product()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Take(10).OrderByDescending(p => p.ListPrice).Select(s => new
                {
                    Name = s.Name,
                    Prize = s.ListPrice
                });

                foreach (var item in list)
                {
                    Console.WriteLine($"Product Name : {item.Name,-10}, Prize : {item.Prize}");
                }
            }
        }

        public void LondonCustomer()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Customers
                            .Include(c => c.Person)
                                .ThenInclude(p => p.BusinessEntity)
                                    .ThenInclude(be => be.BusinessEntityAddresses)
                                        .ThenInclude(ba => ba.Address)
                            .Where(c => c.Person.BusinessEntity.BusinessEntityAddresses
                                         .Any(ba => ba.Address.City == "London"))
                            .Select(c => new
                            {
                                CustomerID = c.CustomerId,
                                LastName = c.Person.LastName,
                                City = c.Person.BusinessEntity.BusinessEntityAddresses
                                           .Select(ba => ba.Address.City)
                                           .FirstOrDefault()
                            })
                            .OrderBy(e => e.LastName)
                            .ToList();


                foreach (var item in list)
                {
                    Console.WriteLine(item.CustomerID + " " + item.LastName + " " + item.City);
                }
            }
        }

        public void OutofStock()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Where(p => p.SafetyStockLevel == 0).Select(e => new
                {
                    ProductName = e.Name
                });

                foreach (var item in list)
                {
                    Console.WriteLine(item.ProductName);
                }
            }
        }

        public void recentorder()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesOrderHeaders.Select(e => new
                {
                    OrderID = e.SalesOrderId,
                    OrderDate = e.OrderDate,
                    Name = e.Customer.Person.FirstName
                }).OrderByDescending(e => e.OrderDate)
                .Take(5)
                .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"Oder ID : {item.OrderID}, OrderDate : {item.OrderDate}, Name : {item.Name}");
                }
            }
        }

        public void CustomerIDWithOrder()
        {
            Console.Write("Enter the Customer ID : ");
            int CustomerID = input.IntCheck(Console.ReadLine());

            using (var dbcontext = new AdventureWorksContext())
            {

                //var list = dbcontext.SalesOrderHeaders
                //    .Where(soh => soh.CustomerId == CustomerID)
                //    .Include(soh => soh.SalesOrderDetails)
                //        .ThenInclude(sod => sod.SpecialOfferProduct)
                //            .ThenInclude(sop => sop.Product)
                //    .SelectMany(soh => soh.SalesOrderDetails.Select(sod => new
                //    {
                //        OrderDate = soh.OrderDate,
                //        ProductName = sod.SpecialOfferProduct.Product.Name,
                //        TotalAmount = soh.TotalDue
                //    }))
                //    .ToList();

                var list = from soh in dbcontext.SalesOrderHeaders
                           join sod in dbcontext.SalesOrderDetails
                           on soh.SalesOrderId equals sod.SalesOrderId
                           join sop in dbcontext.SpecialOfferProducts
                           on sod.ProductId equals sop.ProductId
                           join p in dbcontext.Products
                           on sop.ProductId equals p.ProductId
                           where (soh.CustomerId == CustomerID)
                           select new
                           {
                               OrderDate = soh.OrderDate,
                               productName = p.Name,
                               TotalAmount = soh.TotalDue
                           };

                if (list == null)
                {
                    Console.WriteLine("Customer ID not found");
                }
                else
                {
                    foreach (var item in list)
                    {
                        Console.WriteLine($"Order Date : {item.OrderDate}, Product Name : {item.productName}, TotalDue : {item.TotalAmount}");
                    }

                }
            }
        }

        public void empWithManger()
        {

        }

        public void empWithDept()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Employees.Include(e => e.EmployeeDepartmentHistories).ThenInclude(e => e.Department).Select(e=> new
                {
                    Name = e.BusinessEntity.FirstName+" "+e.BusinessEntity.LastName,
                    Department = e.EmployeeDepartmentHistories.FirstOrDefault().Department.Name

                });

                foreach (var item in list)
                {
                    Console.WriteLine($"Employee Name : {item.Name,-15}, Department : {item.Department}");
                }
            }
        }

        public void TotalSaleEachYear()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var List = dbcontext.SalesOrderHeaders.GroupBy(g => g.OrderDate.Year).Select(e => new
                {
                    Year = e.Key,
                    Total = e.Count()
                }).ToList();

                foreach (var item in List)
                {
                    Console.WriteLine($"Year : {item.Year}, Total Sale : {item.Total}");
                }
            }
        }

        public void avgproductSubcat()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.GroupBy(e => e.ProductSubcategoryId)
                    .Select(e => new
                    {
                        Category = e.Key,
                        Name = e.Select(m=> m.ProductSubcategory.Name).FirstOrDefault(),
                        Value = e.Average(m => m.ListPrice)
                    }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"Caegory ID : {item.Category,-10} Name : {item.Name,-20} AvgPrice : {item.Value}");
                }
            }
        }

        public void bestProductwithQuantity()
        {
            using(var dbcontext = new AdventureWorksContext() )
            {
                var list = dbcontext.SalesOrderDetails.GroupBy(e => e.ProductId).Select(e => new
                {
                    ID = e.Key,
                    Name = e.Select(m=> m.SpecialOfferProduct.Product.Name).FirstOrDefault(),
                    Value = e.Sum(m => m.OrderQty)
                }).OrderByDescending(m => m.Value).
                Take(1)
                .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.ID + " : " +item.Name +" - "+ item.Value);
                }
            }
        }

        public void top5SalePeopleIn2013()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesOrderHeaders
                    .Where(e=> e.OrderDate.Year == 2013)
                    .GroupBy(s => s.SalesPersonId)
                .Select(s => new
                {
                    ID = s.Key,
                    Name = s.Select(m => m.SalesPerson.BusinessEntity.BusinessEntity.FirstName).FirstOrDefault(),
                    Count = s.Count()
                })
                .Take(5)
                .OrderByDescending(c => c.Count).ToList();
                    

                foreach (var item in list)
                {
                    Console.WriteLine($"SalePerson ID : {item.ID} Name : {item.Name,-20} Totol : {item.Count}");
                }
            }
        }

        public void NeverBlacedOrder()
        {
            //using (var dbcontext = new AdventureWorksContext())
            //{
            //    var customers = from c in dbcontext.Customers
            //                    join o in dbcontext.SalesOrderHeaders
            //                        on c.CustomerId equals o.CustomerId into gj
            //                    from sub in gj.DefaultIfEmpty()
            //                    where sub == null   
            //                    select c;

            //    foreach (var c in customers)
            //    {
            //        Console.WriteLine($"{c.CustomerId} : {c.Person?.FirstName} {c.Person?.LastName}");
            //    }
            //}

            using (var DBcontext = new AdventureWorksContext())
            {
                var never = DBcontext.Customers.Where(c => c.SalesOrderHeaders.Count == 0).Select(s => new
                {
                    CustomerId = s.CustomerId,
                    CustomerName = s.Person != null ? s.Person.FirstName : "Unknown"
                })
                .OrderBy(o => o.CustomerId)
                .ToList();

                foreach (var item in never)
                {
                    Console.WriteLine("{0,-20} {1,-20}", item.CustomerId, item.CustomerName);
                }
            }
        }

        public void TeritoryandCustomer()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesTerritories
                    .Select(s => new
                    {
                        ID = s.TerritoryId,
                        Name = s.Name,
                        CustomerCount = s.Customers.Count(),  
                        TotalAmount = s.SalesOrderHeaders.Sum(r => r.TotalDue)
                    })
                    .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"TerritoryID : {item.ID,-5} Name : {item.Name,-20} Customers : {item.CustomerCount,-5} TotalSales : {item.TotalAmount}");
                }
            }

        }

        public void SPoutput()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                Console.Write("Enter the ID : ");
                int ID = input.IntCheck(Console.ReadLine());

                var InputID = new SqlParameter("@ID", ID);

                var list = dbcontext.SpOuts.FromSqlRaw("EXEC uspGetEmployeeManagers @ID", InputID);

                foreach (var item in list)
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + " " + item.RecursionLevel + " " + item.ManagerFirstName);
                }
            }
        }
        public void RawSQL()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                string sql = @"SELECT ProductID, Name From Production.Product Where ListPrice > 1000";

                var value = dbcontext.RawSqlProducts.FromSqlRaw(sql).ToList();

                foreach (var item in value)
                {
                    Console.WriteLine(item.ProductID+" : "+item.Name);
                }
            }
        }

        // Practice

        public void DeparmentWithCount()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Departments.Select(s => new DepartmentCount
                {
                    Id = s.DepartmentId,
                    DepartmentName = s.Name,
                    Count = s.EmployeeDepartmentHistories.Count()
                }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.Id + " " + item.DepartmentName + " " + item.Count);
                }
            }
        }

        public void ProductColourNotNULL()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Where(s => s.Color != null).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.Name);   
                }
            }
        }

        public void top20withStand()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Take(20).OrderByDescending(s => s.StandardCost).Select(s => new
                {
                    s.Name,
                    s.StandardCost
                }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.Name + " " + item.StandardCost);
                }
            }
        }

        public void employeeWhoisManger()
        {
            using (var dbconext = new AdventureWorksContext())
            {
                var list = dbconext.Employees.Where(s => s.JobTitle.Contains("Manager")).Select(s => new
                {
                    s.JobTitle,
                    s.BusinessEntity.FirstName
                }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.JobTitle + " " + item.FirstName);
                }
            }
        }

        public void productwithsubandPrentCat()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products.Select(s => new
                {
                    product = s.Name,
                    subcat = s.ProductSubcategory.Name,
                    cat = s.ProductSubcategory.ProductCategory.Name
                }).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine(item.product + " - " + item.subcat + " - " + item.cat);
                }
            }
        }

        public void avgminmax()
        {
            using (var db = new AdventureWorksContext())
            {
                var result = new
                {
                    Highest = db.Products.Max(p => p.ListPrice),
                    Lowest = db.Products.Min(p => p.ListPrice),
                    Average = db.Products.Average(p => p.ListPrice)
                };

                Console.WriteLine(result.Highest);
                Console.WriteLine(result.Lowest);
                Console.WriteLine(result.Average);
            }
        }

        public void countOFEachSubCat()
        {
            using(var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.ProductSubcategories.Select(n=> new
                {
                    name = n.Name,
                    count = n.Products.Count()
                });

                foreach (var item in list)
                {
                    Console.WriteLine(item.name+" - "+item.count);
                }
            }
        }

        public void productMaxstandCost()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var maxCost = dbcontext.Products.Max(s => s.StandardCost);

                var product = dbcontext.Products
                                       .Where(p => p.StandardCost == maxCost)
                                       .FirstOrDefault();

                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Cost: {product.StandardCost}");
            }

        }

        public void avgoforderID()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var avgQty = dbcontext.SalesOrderDetails.Average(s => s.OrderQty);

                Console.WriteLine("Average OrderQty: " + avgQty);
            }

            //using (var dbcontext = new AdventureWorksContext())
            //{
            //    var list = dbcontext.SalesOrderHeaders
            //        .Select(g => new
            //        {
            //            OrderId = g.SalesOrderId,
            //            AvgQty = g.SalesOrderDetails.Average(s=> s.or)
            //        })
            //        .ToList();

            //    foreach (var item in list)
            //    {
            //        Console.WriteLine($"OrderId: {item.OrderId}, AvgQty: {item.AvgQty}");
            //    }
            //}
        }

        public void empwithdptandJontile()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    Name = s.BusinessEntity.FirstName,
                    Department = s.EmployeeDepartmentHistories.Where(s => s.EndDate == null).Select(s => s.Department.Name).FirstOrDefault(),
                    JobRole = s.JobTitle
                }).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.Name + " - " + item.Department + " - " + item.JobRole);
                }
            }
        }

        public void saleorderWitCusandtertery()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesOrderHeaders.Select(s => new
                {
                    id = s.SalesOrderId,
                    Name = s.Customer.Person.FirstName,
                    Ter = s.Territory.TerritoryId
                }).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine(item.id + " " + item.Name + " " + item.Ter);
                }
            }
        }

        public void productwitVendors()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products
                    .SelectMany(p => p.ProductVendors, (p, pv) => new
                    {
                        ProductName = p.Name,
                        VendorName = pv.BusinessEntity.Name
                    })
                    .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"{item.ProductName} - {item.VendorName}");
                }
            }
        }

        public void totalduemorethan3000()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesOrderHeaders.Where(s => s.TotalDue > 5000).ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.SalesOrderId + " " + item.TotalDue);
                }
            }
        }

        public void cusMorethan5()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
              var list = dbcontext.Customers
                    .Where(c => c.SalesOrderHeaders.Count() > 5)
                    .Select(c => new
                    {
                        c.CustomerId,
                        OrderCount = c.SalesOrderHeaders.Count()
                    })
                    .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.CustomerId+" "+item.OrderCount);
                }
            }
        }

        public void terorymorethan10()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.SalesTerritories.Where(w => w.Customers.Count() > 10).Select(s => s.Name);

                foreach (var item in list)
                {
                    Console.WriteLine("Name : " + item);
                }
            }
        }

        public void productNeversold()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var list = dbcontext.Products
                    .Where(p => !p.SpecialOfferProducts
                        .SelectMany(so => so.SalesOrderDetails)
                        .Any()) 
                    .ToList();

                foreach (var prod in list)
                {
                    Console.WriteLine($"ProductId: {prod.ProductId}, Name: {prod.Name}");
                }
            }
        }

        public void top3Cat()
        {
            using (var dbcontext = new AdventureWorksContext())
            {
                var topCategories = dbcontext.ProductCategories
                    .Select(c => new
                    {
                        CategoryName = c.Name,
                        ProductCount = c.ProductSubcategories
                                        .SelectMany(sc => sc.Products)
                                        .Count()
                    })
                    .OrderByDescending(c => c.ProductCount)
                    .Take(3)
                    .ToList();

                foreach (var cat in topCategories)
                {
                    Console.WriteLine($"Category: {cat.CategoryName}, Products: {cat.ProductCount}");
                }
            }

        }

    }
}

