using EF_Assessment.DTO;
using EF_Assessment.Models;
using EF_Assessment.Validations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assessment
{
    public class Operations
    {
        // validations
        InputValidations input = new InputValidations();
        public void AddCustomerRecord()
        {
            Console.Write("Enter the Customer ID : ");
            string checkCustomerID = Console.ReadLine();
            string CustomerID = input.CustomerIDCheck(checkCustomerID).ToUpper();

            Console.Write("Enter the Company Name : ");
            string checkCompanyName = Console.ReadLine();
            string CompanyName = input.CustomerCompanyCheck(checkCompanyName);

            Console.Write("Enter the Contact Name : ");
            string checkContackName = Console.ReadLine();
            string ContactName = input.ContactNameCheck(checkContackName);

            Console.Write("Enter the Contact Title : ");
            string checkContactTile = Console.ReadLine();
            string ContactTile = input.ContactTileCheck(checkContactTile);  

            Console.Write("Enter the Address : ");
            string checkAddress = Console.ReadLine();
            string Address = input.AddressCheck(checkAddress);

            Console.Write("Enter the City : ");
            string checkCity = Console.ReadLine();
            string City = input.CityCheck(checkCity);

            //Console.Write("Enter the Region : ");
            //string checkRegion = Console.ReadLine();

            //Console.Write("Enter the PostalCode : ");
            //string checkPostalCode = Console.ReadLine();

            Console.Write("Enter the Country : ");
            string checkCountry = Console.ReadLine();
            string Country = input.CountryCheck(checkCountry);

            //Console.Write("Enter the Phone Number : ");
            //string checkPhone = Console.ReadLine();

            //Console.Write("Enter the Fax : ");
            //string checkFax = Console.ReadLine();

            var Customer = new AddCustomer
            {
                CustomerId = CustomerID,
                CompanyName = CompanyName,
                ContactName = ContactName,
                ContactTile = ContactTile,
                Address = Address,
                City = City,
                Country = Country
            };

            var NewCustomer = new Customer
            {
                CustomerId = Customer.CustomerId,
                CompanyName = Customer.CompanyName,
                ContactName = Customer.ContactName,
                ContactTitle = Customer.ContactTile,
                Address = Customer.Address,
                City = Customer.City,
                Country = Customer.Country
            };

            try
            {
                using (var DBcontext = new NorthWindContext())
                {
                    DBcontext.Customers.Add(NewCustomer);
                    DBcontext.SaveChanges();
                    Console.WriteLine();
                    Console.WriteLine("Added New Customer Successfully");
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayCustomerOrderCount()
        {
            try
            {
                using (var DBContext = new NorthWindContext())
                {
                    var customerOrders = DBContext.Customers.Select(s => new CustomerOrdersCount
                    {
                        CustomerID = s.CustomerId,
                        CompanyName = s.CompanyName,
                        TotalOrders = s.Orders.Count(),
                    }).ToList();

                    foreach (var item in customerOrders)
                    {
                        Console.WriteLine($"CustomerID : {item.CustomerID,-7} Company Name : {item.CompanyName,-40} Total Orders : {item.TotalOrders}");
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayTop5Expensive()
        {
            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var ExpensiveProducts = DBContext.Products.Select(s => new Top5ExpensiveProducts
                    { 
                        ProductID = s.ProductId,
                        ProductName = s.ProductName,
                        ProductPrice = (decimal)s.UnitPrice
                    })
                        .Take(5).OrderByDescending(o=> o.ProductPrice).ToList();

                    foreach (var item in ExpensiveProducts)
                    {
                        Console.WriteLine($"Product ID : {item.ProductID,-3} Product Name : {item.ProductName,-25} Product Price : {item.ProductPrice}");
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayEmployeeOrders()
        {
            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var EmployeeOrders = DBContext.Employees.Select(s => new EmployeeOrders
                    {
                        EmployeeID = s.EmployeeId,
                        FullName = s.FirstName + " " + s.LastName,
                        Orders = s.Orders.Count()
                    }).ToList();

                    foreach(var item in EmployeeOrders)
                    {
                        Console.WriteLine($"Employee ID : {item.EmployeeID,-3} Employee Name : {item.FullName, -25} Orders Handled : {item.Orders}");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CustomerwithZeroOrders()
        {
            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var Customers = DBContext.Customers.Where(c => !c.Orders.Any()).Select(s => new CustomerZeroOrders
                    {
                        CustomerID = s.CustomerId,
                        CompanyName = s.CompanyName
                    }).ToList();

                    foreach (var item in Customers)
                    {
                        Console.WriteLine($"Customer ID : {item.CustomerID,-5}, Company Name : {item.CompanyName}");
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Sp_CustOrderHist()
        {
            Console.Write("Enter the Customer ID : ");
            string checkCustomerID = Console.ReadLine();
            string CustomerID =input.CustomerIDCheck(checkCustomerID);

            var CustomerIDinput = new SqlParameter("@customerID", CustomerID);

            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var Result = DBContext.SP_CustOrderHists.FromSqlRaw("EXEC CustOrderHist @customerID", CustomerIDinput).ToList();

                    if(Result.Count() == 0)
                    {
                        Console.WriteLine("CustoemrID not found or This Customer Not Order anything");
                        Console.WriteLine();
                    }
                    else
                    {
                        foreach (var item in Result)
                        {
                            Console.WriteLine($"ProductName : {item.ProductName,-35} Total : {item.Total}");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ProductWithCategory()
        {
            Console.Write("Enter the Category Name : ");
            string CategoryName = Console.ReadLine().Trim().ToLower();

            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var ProductsCategory = DBContext.Products.Where(p => p.Category.CategoryName.Contains(CategoryName)).Select(s => 
                    new ProductCategory
                    {
                        ProductID = s.ProductId,
                        ProductName = s.ProductName,
                        CategoryName = s.Category.CategoryName
                    }).ToList();

                    if(ProductsCategory.Count() == 0)
                    {
                        Console.WriteLine("No Category Match ");
                        Console.WriteLine();
                    }
                    else
                    {
                        foreach (var item in ProductsCategory)
                        {
                            Console.WriteLine($"Product ID : {item.ProductID,-3} Product Name : {item.ProductName,-40} Category Name : {item.CategoryName} ");
                        }
                    }

                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void UpdateAddressOfEmp()
        {
            Console.Write("Enter the Employee ID : ");
            string checkEmpID = Console.ReadLine();
            int EmployeeID = input.IntCheck(checkEmpID);

            try
            {
                using(var DBContext = new NorthWindContext())
                {
                    var updateEmpAddress = DBContext.Employees.Where(e => e.EmployeeId == EmployeeID).ToList();

                    string Name = "";

                    if(updateEmpAddress.Count() == 0)
                    {
                        Console.WriteLine("Employee ID not Found");
                    }
                    else
                    {
                        Console.Write("Enter the Address to Update (less than 60 char) : ");
                        string checkAddress = Console.ReadLine();
                        string Address = input.AddressCheck(checkAddress);

                       foreach (var item in updateEmpAddress)
                       {
                            item.Address = Address;
                            Name = item.FirstName + " " + item.LastName;
                       }

                        DBContext.SaveChanges();
                        Console.WriteLine();
                        Console.WriteLine($"Hello {Name}, your Address changed Succesfully");
                        Console.WriteLine();
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
