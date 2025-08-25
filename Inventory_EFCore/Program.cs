using EFCore_DBFirstApp;
using EFCore_DBFirstApp.Models;
using System;

namespace EFCore_DBFirsstApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            InputsGetters inGet = new InputsGetters();

            while (true)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("--------------------- OPTIONS ---------------------");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(" 1.                          Add Product ");
                Console.WriteLine(" 2.                          Check Product Stock");
                Console.WriteLine(" 3.                          Availabe Products-Cat");
                Console.WriteLine(" 4.                          Insert Product Review");
                Console.WriteLine(" 5.                          Bulk Prouduct Insert");
                Console.WriteLine(" 6.                          Update Prize");
                Console.WriteLine(" 7.                          Puchase Product ");
                Console.WriteLine(" 8.                          Dapper");
                Console.WriteLine(" 9.                          Exit");
                Console.WriteLine("---------------------------------------------------");

                Console.WriteLine();
                Console.Write("Enter the Option code : ");
                string Code = Console.ReadLine();

                switch(Code)
                {
                    case "1":
                        inGet.addProuducts();
                        break;
                    case "2":
                        inGet.checkStockInput();
                        break;
                    case "3":
                        inGet.ProductwithCatInput();
                        break;
                    case "4":
                        inGet.insertReview();
                        break;
                    case "5":
                        inGet.InsertBulkProductsinput();
                        break;
                    case "6":
                        inGet.UpdatePrizeinput();
                        break;
                    case "7":
                        inGet.PurchaseProduct();
                        break;
                    case "8":
                        inGet.DapperInput();
                        break;
                    case "9":
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Try Again");
                        break;
                }
            }
        }
    }
}