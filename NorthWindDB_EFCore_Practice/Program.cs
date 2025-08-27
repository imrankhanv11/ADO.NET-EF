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