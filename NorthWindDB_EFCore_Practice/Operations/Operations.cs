using Excersice2_EFCoreNorthWind.Models;
using Excersice2_EFCoreNorthWind.Validation;
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
            Console.Write("Enter the Manager First Name : ");
            string FirstNameemp = input.FirstNameCheck(Console.ReadLine());

            Console.Write("Enter the Manager Last Name : ");
            string LastNameemp = input.LastNameCheck(Console.ReadLine());

            Console.WriteLine("Enter the Title : ");
            string Titleemp = input.TitleCheck(Console.ReadLine());

            string TitleofCoursceemp = "";
            bool checkemp = true;
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
    }
}
