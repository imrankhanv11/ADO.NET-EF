using Azure;
using Excersice2_EFCoreNorthWind.Models;
using System;

namespace Excersice2_EFCoreNorthWind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OperationProcess op = new OperationProcess();
            while (true)
            {
                Console.WriteLine("+-------------------------------------------------------------------------------------+");
                Console.WriteLine("|                                        MENU                                         |");
                Console.WriteLine("+-------------------------------------------------------------------------------------+");
                Console.WriteLine("|  1.                            Place new order                                      |");
                Console.WriteLine("|  2.                            Create new category with multiple products           |");
                Console.WriteLine("|  3.                            Add a new employee with manager (both should be new) |");
                Console.WriteLine("|  4.                            Add a new employee to existing manager               |");
                Console.WriteLine("|  5.                            Update existing emplyee to new manager               |");
                Console.WriteLine("|  6.                            Customers who placed orders in 1997 but not in 1998. |");
                Console.WriteLine("|  7.                            List customers and their most recent order date.     |");
                Console.WriteLine("|  8.                            customers whose total order value exceeds 50000.     |");
                Console.WriteLine("|  9.                            category with the average unit price of products     |");
                Console.WriteLine("| 10.                            products that have never been ordered.               |");
                Console.WriteLine("| 11.                            top 3 most ordered products (by total quantity sold) |");
                Console.WriteLine("| 12.                            products along with their supplier name and category |");
                Console.WriteLine("| 13.                            products where UnitPrice > Category Average Price.   |");
                Console.WriteLine("| 14.                            employees with the total sales amount they handled   |");
                Console.WriteLine("| 15.                            employee who handled the most orders in 1997         |");
                // Not Completed
                Console.WriteLine("| 16.                            employees who share the same territory               |");
                Console.WriteLine("| 17.                            employee with number distinct customer they served   |");
                Console.WriteLine("| 18.                            employees with the first order they ever handled.    |");
                Console.WriteLine("| 19.                            shipper, average delivery time                       |");
                Console.WriteLine("| 20.                            orders that took more than 30 days to deliver        |");
                Console.WriteLine("| 21.                            top shipper based on the number of orders shipped.   |");
                // Not Completed
                Console.WriteLine("| 22.                            top employee per year based on total sales           |");
                Console.WriteLine("| 23.                            products that were ordered by every customer         |");
                Console.WriteLine("| 24.                            suppliers who supply more than 5 products.           |");
                Console.WriteLine("| 25.                            customer(s) with the single highest order value      |");
                Console.WriteLine("|  0.                                        EXIT                                     |");
                Console.WriteLine("+-------------------------------------------------------------------------------------+");
                Console.WriteLine();
                Console.Write("Enter the Code : ");
                string Code = Console.ReadLine();

                switch(Code)
                {
                    case "1":
                        op.PlaceOrder();
                        break;
                    case "2":
                        op.CatWithProduct();
                        break;
                    case "3":
                        op.MangerWithEmpBOTH();
                        break;
                    case "4":
                        op.InsertEmpwitMangerONE();
                        break;
                    case "5":
                        op.UpdateEmpManger();
                        break;
                    case "6":
                        op.FindCustomerBetween();
                        break;
                    case "7":
                        op.CustomerRecentOrder();
                        break;
                    case "8":
                        op.Order5000();
                        break;
                    case "9":
                        op.CatAVG();
                        break;
                    case "10":
                        op.NotOrderProduct();
                        break;
                    case "11":
                        op.Top3orderProduct();
                        break;
                    case "12":
                        op.ProductCATsup();
                        break;
                    case "13":
                        op.UnitAVGCAt();
                        break;
                    case "14":
                        op.EmpwithTotalSaleAmount();
                        break;
                    case "15":
                        op.empMOST1997();
                        break;
                    case "16":
                        op.empSameTER();
                        break;
                    case "17":
                        op.DisCusEmp();
                        break;
                    case "18":
                        op.EmpWithFirstOrder();
                        break;
                    case "19":
                        op.shipperAVGdel();
                        break;
                    case "20":
                        op.shipOrderMoreThan30();
                        break;
                    case "21":
                        op.topShipper();
                        break;
                    case "22":
                        op.topEmpYear();
                        break;
                    case "23":
                        op.ProductWithallCus();
                        break;
                    case "24":
                        op.SupplierMoreThan5();
                        break;
                    case "25":
                        op.CusSingleHighest();
                        break;

                    case "0":
                        Console.WriteLine("Thank you");
                        return;
                    default:
                        Console.WriteLine("Try Again");
                        break;
                }

            }
        }
    }
}