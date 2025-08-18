using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //read
            CrudOp crud = new CrudOp();

            //bulk 
            Bulk bulk = new Bulk();

            // linq
            Linq linq = new Linq();

            while (true)
            {

                Console.WriteLine("---------------------------------");
                Console.WriteLine("-------------| Menu |------------");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("| 1.                 BASIC CRUD |");
                Console.WriteLine("| 2.                 BULK CRUD  |");
                Console.WriteLine("| 3.                 LINQ       |");
                Console.WriteLine("| 4.                 EXIT       |");
                Console.WriteLine("---------------------------------");

                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        crud.crudmethod();
                        break;
                    case "2":
                        await bulk.crudmethod();
                        break;
                    case "3":
                        linq.linqMethod();
                        break;
                    case "4":
                        Console.WriteLine("Thank you");
                        return;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }

        }
    }
}
