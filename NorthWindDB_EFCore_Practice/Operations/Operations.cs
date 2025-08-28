using Excersice2_EFCoreNorthWind.Models;
using Excersice2_EFCoreNorthWind.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersice2_EFCoreNorthWind
{
    public class OperationProcess
    {
        InputValidations input = new InputValidations();
        public void PlaceOrder()
        {
            List<OrderDetail> orders = new List<OrderDetail>();
            string checknewone;
            do
            {
                Console.WriteLine("Product List : ");
                int ProductID;
                decimal unitprize;
                using (var dbcontext = new NorthWindContext())
                {
                    var list = dbcontext.Products.Select(s => new
                    {
                        ID = s.ProductId,
                        Name = s.ProductName
                    }).ToList();

                    foreach (var item in list)
                    {
                        Console.WriteLine(item.ID + " : " + item.Name);
                    }
                RECHECK_productID:
                    Console.Write("Enter the Product ID : ");
                    ProductID = input.IntCheck(Console.ReadLine());

                    if (!dbcontext.Products.Any(s => s.ProductId == ProductID))
                    {
                        Console.WriteLine("Product ID not Found");
                        goto RECHECK_productID;
                    }

                    var list2 = dbcontext.Products.Find(ProductID);

                    unitprize =(decimal) list2.UnitPrice;

                }

                Console.Write("Enter the Quantity : ");
                int Quantity = input.IntCheck(Console.ReadLine());

                Console.Write("Enter the Discount : ");
                decimal Discount = input.DiscountCheck(Console.ReadLine());

                

                var productlist = new OrderDetail
                {
                    ProductId = ProductID,
                    UnitPrice = unitprize,
                    Quantity = (short)Quantity,
                    Discount = (float)Discount
                };

                orders.Add(productlist);

                Console.Write("Do you want to Add new product (yes/no) : ");
                checknewone = Console.ReadLine().Trim().ToLower();
            } while (checknewone == "yes");

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

                foreach (var item in orders)
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

        public void CatWithProduct()
        {
            Console.Write("Enter the Category Name : ");
            string CategoryName = input.CatCheck(Console.ReadLine());

            Console.Write("Enter the Discription : ");
            string Discription = input.DisCheck(Console.ReadLine());

            var cat = new Category
            {
                CategoryName = CategoryName,
                Description = Discription
            };

            List<Product> products = new List<Product>();

            using(var dbcontext = new NorthWindContext())
            {
                using(var transaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        dbcontext.Categories.Add(cat);
                        dbcontext.SaveChanges();

                        int CatID = cat.CategoryId;

                        var list = dbcontext.Suppliers.ToList();

                        string checkAgain;
                        do
                        {
                            Console.Write("Enter the Product Name : ");
                            string ProductName = input.ProductCheck(Console.ReadLine());
                        CHECKAGAIN_SupID:
                            Console.WriteLine("Supplier List : ");
                            foreach (var item in list)
                            {
                                Console.WriteLine(item.SupplierId + " " + item.CompanyName);
                            }

                            Console.Write("Enter the Supplier ID : ");
                            int SupplierID = input.IntCheck(Console.ReadLine());

                            if (!dbcontext.Suppliers.Any(a => a.SupplierId == SupplierID))
                            {
                                Console.WriteLine("Supplier ID not Found");
                                goto CHECKAGAIN_SupID;
                            }

                            Console.Write("Enter the Quanitity Per Unit : ");
                            string QuantityPerUnit = input.QutityperunitCheck(Console.ReadLine());

                            Console.Write("Enter the Unit Per Prize : ");
                            decimal UnitperPrize = input.DecimalCheck(Console.ReadLine());

                            Console.Write("Enter the Unit in stock : ");
                            int QuntitiyInStock = input.IntCheck(Console.ReadLine());

                            var productLIst = new Product
                            {
                                ProductName = ProductName,
                                SupplierId = SupplierID,
                                CategoryId = CatID,
                                QuantityPerUnit = QuantityPerUnit,
                                UnitPrice = UnitperPrize,
                                UnitsInStock = (short)QuntitiyInStock,
                                UnitsOnOrder = 0,
                                ReorderLevel = 0,
                            };

                            products.Add(productLIst);

                            Console.WriteLine("Do you want to add again (yes/no) : ");
                            checkAgain = Console.ReadLine().ToLower().Trim();
                        } while (checkAgain == "yes");

                        dbcontext.Products.AddRange(products);
                        dbcontext.SaveChanges();
                        Console.WriteLine("Succesfully added");
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void MangerWithEmpBOTH()
        {
            Console.WriteLine("-----> Enter Manager Details <----");
            Console.WriteLine();
            Console.Write("Enter the Manager First Name : ");
            string FirstName = input.FirstNameCheck(Console.ReadLine());

            Console.Write("Enter the Manager Last Name : ");
            string LastName = input.LastNameCheck(Console.ReadLine());

            Console.WriteLine("Enter the Title : ");
            string Title = input.TitleCheck(Console.ReadLine());

            string TitleofCoursce ="";
            bool check = true;
            while (check)
            {
                Console.WriteLine("List of Tile of curtersy : ");
                Console.WriteLine("1. Ms. ");
                Console.WriteLine("2. Mr.");
                Console.WriteLine("3. Dr.");

                Console.Write("Enter the code to assign : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        TitleofCoursce = "Ms.";
                        check = false;
                        break;
                    case "2":
                        TitleofCoursce = "Mr.";
                        check = false;
                        break;
                    case "3":
                        TitleofCoursce = "Dr.";
                        check = false;
                        break;
                    default:
                        Console.WriteLine("not match try again");
                        break;
                }
            }

            DateTime BirthDate = DateTime.Now;
            DateTime HireDate = DateTime.Now;

            Console.Write("Enter the Address : ");
            string Address = input.AddressCheck(Console.ReadLine());

            Console.Write("Enter the City : ");
            string City = input.CityCheck(Console.ReadLine());

            Console.Write("Enter the Country : ");
            string Country = input.CountryCheck(Console.ReadLine());

            var manager = new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                Title = Title,
                TitleOfCourtesy = TitleofCoursce,
                BirthDate = BirthDate,
                HireDate = HireDate,
                Address = Address,
                City = City,
                Country = Country
            };

            // employee

            Console.WriteLine("-----> Enter Employee Details <----");
            Console.WriteLine();
            Console.Write("Enter the Employee First Name : ");
            string FirstNameemp = input.FirstNameCheck(Console.ReadLine());

            Console.Write("Enter the Employee Last Name : ");
            string LastNameemp = input.LastNameCheck(Console.ReadLine());

            Console.WriteLine("Enter the Title : ");
            string Titleemp = input.TitleCheck(Console.ReadLine());

            string TitleofCoursceemp = "";
            bool checkemp = true;
            while (checkemp)
            {
                Console.WriteLine("List of Tile of curtersy : ");
                Console.WriteLine("1. Ms. ");
                Console.WriteLine("2. Mr.");
                Console.WriteLine("3. Dr.");

                Console.Write("Enter the code to assign : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        TitleofCoursceemp = "Ms.";
                        checkemp = false;
                        break;
                    case "2":
                        TitleofCoursceemp = "Mr.";
                        checkemp = false;
                        break;
                    case "3":
                        TitleofCoursceemp = "Dr.";
                        checkemp = false;
                        break;
                    default:
                        Console.WriteLine("not match try again");
                        break;
                }
            }

            DateTime BirthDateemp = DateTime.Now;
            DateTime HireDateemp = DateTime.Now;

            Console.Write("Enter the Address : ");
            string Addressemp = input.AddressCheck(Console.ReadLine());

            Console.Write("Enter the City : ");
            string Cityemp = input.CityCheck(Console.ReadLine());

            Console.Write("Enter the Country : ");
            string Countryemp = input.CountryCheck(Console.ReadLine());

            

            using(var dbcontext = new NorthWindContext())
            {
                dbcontext.Employees.Add(manager);
                dbcontext.SaveChanges();
                int ID = manager.EmployeeId;

                var employee = new Employee
                {
                    FirstName = FirstNameemp,
                    LastName = LastNameemp,
                    Title = Titleemp,
                    TitleOfCourtesy = TitleofCoursceemp,
                    BirthDate = BirthDateemp,
                    HireDate = HireDateemp,
                    Address = Addressemp,
                    City = Cityemp,
                    Country = Countryemp,
                    ReportsTo = ID
                };

                dbcontext.Employees.Add(employee);
                dbcontext.SaveChanges();

            }

        }

        public void InsertEmpwitMangerONE()
        {
            Console.Write("Enter the Employee First Name : ");
            string FirstNameemp = input.FirstNameCheck(Console.ReadLine());

            Console.Write("Enter the Employee Last Name : ");
            string LastNameemp = input.LastNameCheck(Console.ReadLine());

            Console.WriteLine("Enter the Title : ");
            string Titleemp = input.TitleCheck(Console.ReadLine());

            string TitleofCoursceemp = "";
            bool checkemp = true;
            while (checkemp)
            {
                Console.WriteLine("List of Tile of curtersy : ");
                Console.WriteLine("1. Ms. ");
                Console.WriteLine("2. Mr.");
                Console.WriteLine("3. Dr.");

                Console.Write("Enter the code to assign : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        TitleofCoursceemp = "Ms.";
                        checkemp = false;
                        break;
                    case "2":
                        TitleofCoursceemp = "Mr.";
                        checkemp = false;
                        break;
                    case "3":
                        TitleofCoursceemp = "Dr.";
                        checkemp = false;
                        break;
                    default:
                        Console.WriteLine("not match try again");
                        break;
                }
            }

            int ID;
            Console.WriteLine("Manager List :");
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.ToList();

            CHECK_mgrID:
                foreach (var item in list)
                {
                    Console.WriteLine(item.EmployeeId + " : " + item.FirstName + " " + item.LastName);
                }

                Console.Write("Enter the Manager ID to assign : ");
                ID = input.IntCheck(Console.ReadLine());

                if(!dbcontext.Employees.Any(a=> a.EmployeeId == ID))
                {
                    Console.WriteLine("Manager ID not found ");
                    goto CHECK_mgrID;
                }
            }

            DateTime BirthDateemp = DateTime.Now;
            DateTime HireDateemp = DateTime.Now;

            Console.Write("Enter the Address : ");
            string Addressemp = input.AddressCheck(Console.ReadLine());

            Console.Write("Enter the City : ");
            string Cityemp = input.CityCheck(Console.ReadLine());

            Console.Write("Enter the Country : ");
            string Countryemp = input.CountryCheck(Console.ReadLine());



            using (var dbcontext = new NorthWindContext())
            {
                var employee = new Employee
                {
                    FirstName = FirstNameemp,
                    LastName = LastNameemp,
                    Title = Titleemp,
                    TitleOfCourtesy = TitleofCoursceemp,
                    BirthDate = BirthDateemp,
                    HireDate = HireDateemp,
                    Address = Addressemp,
                    City = Cityemp,
                    Country = Countryemp,
                    ReportsTo = ID
                };

                dbcontext.Employees.Add(employee);
                dbcontext.SaveChanges();

            }
        }

        public void UpdateEmpManger()
        {
            using(var dbcontext = new NorthWindContext())
            {
            CHECK_empID:
                Console.Write("Enter the Employee ID to Update : ");
                int EmployeeID = input.IntCheck(Console.ReadLine());

                if(!dbcontext.Employees.Any(a=> a.EmployeeId == EmployeeID))
                {
                    Console.WriteLine("Emplyee not found try again");
                    goto CHECK_empID;
                }

                var list = dbcontext.Employees.ToList();

                foreach (var item in list)
                {
                    Console.WriteLine(item.EmployeeId + " : " + item.FirstName + " " + item.LastName);
                }

                Console.Write("Enter the Manger ID to update : ");
                int ManagerID = input.IntCheck(Console.ReadLine());

                var UpdateEmp = dbcontext.Employees.Where(w => w.EmployeeId == EmployeeID).ToList();

                foreach(var item in UpdateEmp)
                {
                    item.ReportsTo = ManagerID;
                }

                dbcontext.SaveChanges();
                Console.WriteLine("Sucessfully Changed the managerID ");
            }
        }

        public void FindCustomerBetween()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Customers
                        .Where(c =>
                            c.Orders.Any(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1997) &&
                            !c.Orders.Any(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == 1998)
                        )
                        .ToList();
                foreach (var item in list)
                {
                    Console.WriteLine($"CustomerID : {item.CustomerId}, CompanyName : {item.CompanyName}");
                }

            }
        }

        public void CustomerRecentOrder()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Customers.Select(s => new
                {
                    CustomerID = s.CustomerId,
                    CompanyName = s.CompanyName,
                    OrderDate = s.Orders.Max(o => o.OrderDate)
                });

                foreach (var item in list)
                {
                    Console.WriteLine($"CustomerID : {item.CustomerID} Company Name : {item.CompanyName,-25} OrderDate : {item.OrderDate}");
                }
            }
        }
        
        public void Order5000()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Customers
                        .Select(c => new
                        {
                            c.CustomerId,
                            c.CompanyName,
                            TotalAmount = c.Orders
                                .SelectMany(o => o.OrderDetails)
                                .Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                        })
                        .Where(x => x.TotalAmount > 50000)
                        .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"CustomerID : {item.CustomerId} CompanyName : {item.CompanyName,-30} Total Amount : {item.TotalAmount}");
                }
            }
        }

        public void CatAVG()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Categories.Select(s => new
                {
                    Name = s.CategoryName,
                    Avg = s.Products.Average(s => s.UnitPrice)
                });

                foreach (var item in list)
                {
                    Console.WriteLine($"Category Name : {item.Name,-20} AVG Amount : {item.Avg}");
                }

            }
        }

        public void NotOrderProduct()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Products.Where(s => s.OrderDetails.Count() == 0).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"Product ID : {item.ProductId}, Product Name : {item.ProductName}");
                }
            }
        }

        public void Top3orderProduct()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Products.Select(s => new
                {
                    ID = s.ProductId,
                    Name = s.ProductName,
                    Quanity = s.OrderDetails.Sum(s => s.Quantity)
                }).OrderByDescending(o => o.Quanity)
                .Take(3).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"ID : {item.ID,-3} Name : {item.Name,-20} Quantity : {item.Quanity}");
                }
            }
        }

        public void ProductCATsup()
        {
            using (var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Products.Select(s => new
                {
                    Id = s.ProductId,
                    Name = s.ProductName,
                    Supplier = s.Supplier.CompanyName,
                    Cat = s.Category.CategoryName
                }).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"Id : {item.Id,-4} Product Name : {item.Name,-20} Supplier Name : {item.Supplier,-20} Category : {item.Cat} ");
                }
            }
        }

        public void UnitAVGCAt()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Products.Select(s => new
                {
                    Id = s.ProductId,
                    Name = s.ProductName,
                    Price = s.UnitPrice,
                    Cat = s.Category.Products.Average(s => s.UnitPrice)
                }).Where(s => s.Price > s.Cat).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine(item.Id + " " + item.Name + " " + item.Price + " " + item.Cat);
                }
            }
        }

        public void EmpwithTotalSaleAmount()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    Id = s.EmployeeId,
                    Name = s.FirstName + " " + s.LastName,
                    Total = s.Orders.SelectMany(s => s.OrderDetails).Sum(s => s.UnitPrice * s.Quantity * (1 - (decimal)s.Discount))
                }).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"ID : {item.Id,-3} Name : {item.Name,-20} Total Amount {item.Total}");
                }
            }
        }

        public void empMOST1997()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    Id = s.EmployeeId,
                    Name = s.FirstName + " " + s.LastName,
                    TotalOrder = s.Orders.Count()
                }).OrderByDescending(s=> s.TotalOrder).Take(1).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"Id : {item.Id,-4} Name : {item.Name,-25} Total Order Handled : {item.TotalOrder}");
                }
            }
        }

        public void empSameTER()
        {
            
        }

        public void DisCusEmp()
        {
            using (var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    ID = s.EmployeeId,
                    Name = s.FirstName + " " + s.LastName,
                    Count = s.Orders.Select(s => s.Customer).Distinct().Count()
                }).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine($"ID : {item.ID - 3} Name : {item.Name,-20} Distint Count : {item.Count}");
                }
            }
        }

        public void EmpWithFirstOrder()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Employees.Select(s => new
                {
                    Id = s.EmployeeId,
                    Name = s.FirstName + " " + s.LastName,
                    OrderDate = s.Orders.Min(s => s.OrderDate)
                }).ToList();

                foreach( var item in list)
                {
                    Console.WriteLine($"ID : {item.Id, -3} Name : {item.Name,-20} Order Date : {item.OrderDate}");
                }
            }
        }

        public void shipperAVGdel()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Shippers
                        .Select(s => new
                        {
                            Name = s.CompanyName,
                            AVGShipTime = s.Orders
                                .Average(o => EF.Functions.DateDiffDay(o.OrderDate, o.ShippedDate))
                        })
                        .ToList();

                foreach( var item in list)
                {
                    Console.WriteLine(item.Name + " --> " + item.AVGShipTime);
                }
            }
        }

        public void shipOrderMoreThan30()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Orders.Where(s => EF.Functions.DateDiffDay(s.OrderDate, s.ShippedDate) > 30)
                    .Select(s => new 
                    {
                        id = s.OrderId,
                        CustomerName = s.Customer.CompanyName,
                        DateDiff = EF.Functions.DateDiffDay(s.OrderDate, s.ShippedDate)
                    }).ToList();


                foreach ( var item in list)
                {
                    Console.WriteLine($"ID : {item.id,-4} CustomerName : {item.CustomerName,-25} Time : {item.DateDiff}");
                }
            }
        }

        public void topShipper()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Shippers.Select(s => new
                {
                    Name = s.CompanyName,
                    Orders = s.Orders.Count()
                }).OrderByDescending(s => s.Orders).Take(1).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine(item.Name + " - " + item.Orders);
                }
            }
        }

        public void topEmpYear()
        {
            
        }

        public void ProductWithallCus()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var totalCustomers = dbcontext.Customers.Count();

                var list = dbcontext.Products
                    .Where(p =>
                        p.OrderDetails
                            .Select(od => od.Order.CustomerId) 
                            .Distinct()
                            .Count() == totalCustomers        
                    )
                    .Select(p => new
                    {
                        ProductID = p.ProductId,
                        ProductName = p.ProductName
                    })
                    .ToList();

                foreach( var item in list)
                {
                    Console.WriteLine($"ID : {item.ProductID,-4} ProductName : {item.ProductName}");
                }

            }
        }

        public void SupplierMoreThan5()
        {
            using(var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Suppliers
                        .Where(s => s.Products.Count() > 4) // for 5 empty
                        .Select(s => new
                        {
                            SupplierID = s.SupplierId,
                            SupplierName = s.CompanyName,
                            ProductCount = s.Products.Count()
                        })
                        .ToList();

                foreach( var item in list)
                {
                    Console.WriteLine(item.SupplierID + " " + item.SupplierName + " " + item.ProductCount);
                }

            }
        }

        public void CusSingleHighest()
        {
            using(var dbcontext =  new NorthWindContext())
            {
                var orderValues = dbcontext.Orders
                        .Select(o => new
                        {
                            o.OrderId,
                            o.Customer.CompanyName,
                            OrderValue = o.OrderDetails
                                .Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                        });

                var maxOrderValue = orderValues.Max(o => o.OrderValue);

                var result = orderValues
                    .Where(o => o.OrderValue == maxOrderValue)
                    .ToList();

                foreach(var  item in result)
                {
                    Console.WriteLine(item.CompanyName+" "+item.OrderId+" "+item.OrderValue);
                }
            }
        }

        public void CatOrderCus()
        {
            using(var dbcontext = new NorthWindContext())
            {
                Console.Write("Choose the Category : ");
                var cat = dbcontext.Categories.ToList();

            CHECK_catID:
                foreach (var item in cat)
                {
                    Console.WriteLine(item.CategoryId + " : " + item.CategoryName);
                }

                Console.Write("Enter the Cat ID : ");
                int categoryId = input.IntCheck(Console.ReadLine());

                if(!dbcontext.Categories.Any(a=> a.CategoryId == categoryId))
                {
                    Console.WriteLine("Cat ID not Found try again");
                    goto CHECK_catID;
                }

                var list = dbcontext.Customers
                    .Where(c =>
                        dbcontext.Products
                            .Where(p => p.CategoryId == categoryId)
                            .All(p => c.Orders
                                .SelectMany(o => o.OrderDetails)
                                .Any(od => od.ProductId == p.ProductId))
                    )
                    .Select(c => new
                    {
                        CustomerName = c.CompanyName,
                        CategoryName = dbcontext.Categories
                                        .Where(cat => cat.CategoryId == categoryId)
                                        .Select(cat => cat.CategoryName)
                                        .FirstOrDefault()
                    })
                    .ToList();

                foreach (var item in list)
                {
                    Console.WriteLine($"Customer Name : {item.CustomerName,-20} {"--",-2} {item.CategoryName}");
                }
            }
        }

        public void MostProfitProduct()
        {
            using (var dbcontext = new NorthWindContext())
            {
                var list = dbcontext.Products.Select(s => new
                {
                    s.ProductId,
                    s.ProductName,
                    Total = s.OrderDetails.Sum(s => s.UnitPrice * s.Quantity * (1 - (decimal)s.Discount))
                }).OrderByDescending(s => s.Total).Take(1).ToList();

                foreach(var item in list)
                {
                    Console.WriteLine(item.ProductId + " " + item.ProductName + " " + item.Total);
                }
            }
        }
    }
}
