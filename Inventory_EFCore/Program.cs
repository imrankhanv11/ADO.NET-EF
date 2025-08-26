using Excersice_EFCore.Models;
using System;

namespace Excersice_EFCore
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Input gettters
            InputGetters input = new InputGetters();

            while(true)
            {
                Console.WriteLine("-------------------------------------------- MENU --------------------------------------------");
                Console.WriteLine(" 01.                All Products (Name, ProductNumber, ListPrice).");
                Console.WriteLine(" 02.                Get all employees hired after 2010.");
                Console.WriteLine(" 03.                Retrieve the top 10 most expensive products.");
                Console.WriteLine(" 04.                Find all customers from London and order them by last name.");
                Console.WriteLine(" 05.                List all products that are out of stock (SafetyStockLevel = 0).");
                Console.WriteLine(" 06.                Get the 5 most recent orders placed.");
                Console.WriteLine(" 07.                Get all orders for a given customer (include order date, product names, and total due)."); // not try in include
                
                // not complete
                Console.WriteLine(" 08.                List all employees along with their managers’ names.");

                Console.WriteLine(" 09.                Show the department name for each employee.");
                Console.WriteLine(" 10.                Find the total sales for each year.");
                Console.WriteLine(" 11.                Get the average list price of products in each subcategory.");
                Console.WriteLine(" 12.                Find the best-selling product by total quantity sold.");
                Console.WriteLine(" 13.                Retrieve the top 5 salespeople with the highest sales in 2013.");
                Console.WriteLine(" 14.                Find customers who have never placed an order.");
                Console.WriteLine(" 15.                For each territory, show the total sales amount and number of customers.");
                Console.WriteLine(" 16.                SP ");
                Console.WriteLine(" 17.                Run a raw SQL query to get products with ListPrice > 1000.");

                // new Practice 
                Console.WriteLine(" 18.                Department with Count ");
                Console.WriteLine(" 19.                Get all products where Color is not null.");
                Console.WriteLine(" 20.                List the top 20 products by StandardCost");
                Console.WriteLine(" 21.                Retrieve all employees whose job title contains Manger");
                Console.WriteLine(" 22.                Show all product subcategories and their parent categories.");
                Console.WriteLine(" 23.                Get the highest, lowest, and average list price of all products");
                Console.WriteLine(" 24.                Count how many products are in each ProductSubcategory");
                Console.WriteLine(" 25.                Find the product with the maximum StandardCost");
                Console.WriteLine(" 26.                Calculate the average OrderQty per sales order.");
                Console.WriteLine(" 27.                List employees with their department and job title");
                Console.WriteLine(" 28.                Get all sales orders along with customer name and territory name");
                Console.WriteLine(" 29.                Show products along with their vendor(s)");
                Console.WriteLine(" 30.                Get all orders where the total due > 5000");
                Console.WriteLine(" 31.                Find customers who placed more than 5 orders.");
                Console.WriteLine(" 32.                List all territories with more than 10 customers.");
                Console.WriteLine(" 33.                Find products that were never ordered.");
                Console.WriteLine(" 34.                List the top 3 product categories by number of products.");
                Console.WriteLine(" 35.                DepartmentID to employeName");
                Console.WriteLine(" 36.                Identify the top 3 customers by total purchases in each territory");
                Console.WriteLine("  0.                                     EXIT");
                Console.WriteLine("----------------------------------------------------------------------------------------------");

                Console.WriteLine();
                Console.Write("Enter the Code : ");
                string Code = Console.ReadLine();

                switch(Code)
                {
                    case "1":
                        input.allProducts();
                        break;
                    case "2":
                        input.gelallEmp();
                        break;
                    case "3":
                        input.Top10product();
                        break;
                    case "4":
                        input.LondonCustomer();
                        break;
                    case "5":
                        input.OutofStock();
                        break;
                    case "6":
                        input.recentorder();
                        break;
                    case "7":
                        input.CustomerIDWithOrder();
                        break;
                    case "8":
                        input.empWithManger();
                        break;
                    case "9":
                        input.empWithDept();
                        break;
                    case "10":
                        input.TotalSaleEachYear();
                        break;
                    case "11":
                        input.avgproductSubcat();
                        break;
                    case "12":
                        input.bestProductwithQuantity();
                        break;
                    case "13":
                        input.top5SalePeopleIn2013();
                        break;
                    case "14":
                        input.NeverBlacedOrder();
                        break;
                    case "15":
                        input.TeritoryandCustomer();
                        break;
                    case "16":
                        input.SPoutput();
                        break;
                    case "17":
                        input.RawSQL();
                        break;
                    case "18":
                        input.DeparmentWithCount();
                        break;
                    case "19":
                        input.ProductColourNotNULL();
                        break;
                    case "20":
                        input.top20withStand();
                        break;
                    case "21":
                        input.employeeWhoisManger();
                        break;
                    case "22":
                        input.productwithsubandPrentCat();
                        break;
                    case "23":
                        input.avgminmax();
                        break;
                    case "24":
                        input.countOFEachSubCat();
                        break;
                    case "25":
                        input.productMaxstandCost();
                        break;
                    case "26":
                        input.avgoforderID();
                        break;
                    case "27":
                        input.empwithdptandJontile();
                        break;
                    case "28":
                        input.saleorderWitCusandtertery();
                        break;
                    case "29":
                        input.productwitVendors();
                        break;
                    case "30":
                        input.totalduemorethan3000();
                        break;
                    case "31":
                        input.cusMorethan5();
                        break;
                    case "32":
                        input.terorymorethan10();
                        break;
                    case "33":
                        input.productNeversold();
                        break;
                    case "34":
                        input.top3Cat();
                        break;
                    case "35":
                        input.DeptIDtoEmp();
                        break;
                    case "36":
                        input.Tertop3Cus();
                        break;

                    case "0":
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Try again Code not match");
                        break;
                }
            }
        }
    }
}