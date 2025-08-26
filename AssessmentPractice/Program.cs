using EF_Assessment.Models;
using System;

namespace EF_Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Obj - Operations
            Operations Opreation = new Operations();
            while(true)
            {
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine("|                                         #MENU                                        |");
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine("| 1.    |     Add a new record to customers table                                      |");
                Console.WriteLine("| 2.    |     Display a list of all customers and their total number of orders         |");
                Console.WriteLine("| 3.    |     Display the top 5 expensive products.                                    |");
                Console.WriteLine("| 4.    |     Display each employee’s full name and number of orders they handled.     |");
                Console.WriteLine("| 5.    |     Display all customers who didn't place any orders                        |");
                Console.WriteLine("| 6.    |     Execute 'CustOrderHist' stored procedure and display the result          |");
                Console.WriteLine("| 7.    |     Display all products with category contains the text entered by the user.|");
                Console.WriteLine("| 8.    |     For the given EmployeeId update the address                              |");
                Console.WriteLine("| 0.    |     EXIT                                                                     |");
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine();
                Console.Write("Enter the Code : ");
                string Code = Console.ReadLine();

                switch (Code)
                {
                    case "1":
                        Opreation.AddCustomerRecord();
                        break;
                    case "2":
                        Opreation.DisplayCustomerOrderCount();
                        break;
                    case "3":
                        Opreation.DisplayTop5Expensive();
                        break;
                    case "4":
                        Opreation.DisplayEmployeeOrders();
                        break;
                    case "5":
                        Opreation.CustomerwithZeroOrders();
                        break;
                    case "6":
                        Opreation.Sp_CustOrderHist();
                        break;
                    case "7":
                        Opreation.ProductWithCategory();
                        break;
                    case "8":
                        Opreation.UpdateAddressOfEmp();
                        break;
                    case "0":
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Try again Code not found");
                        break;
                }
            }
        }
    }
}