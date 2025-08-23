using DbFirst_EFCore_01;
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
                Console.WriteLine(" 3. ");
                Console.WriteLine(" 4.                                 Exit");
                Console.WriteLine("---------------------------------------------------");

                Console.WriteLine();
                Console.WriteLine("Enter the Option code : ");
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

                        break;
                    case "4":
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