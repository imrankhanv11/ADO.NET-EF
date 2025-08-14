using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstApproach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //read
            CrudOp crud = new CrudOp();

            Console.WriteLine("---------------------------------");
            Console.WriteLine("-------------| Menu |------------");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| 1.                 Basic CRUD |");
            Console.WriteLine("| 5.                 EXIT       |");
            Console.WriteLine("---------------------------------");

            while(true)
            {
                Console.Write("Enter the Code to Run : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        crud.crudmethod();
                        break;
                    case "2":
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
