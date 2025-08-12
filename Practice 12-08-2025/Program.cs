using System;
using System.Data.SqlClient;

namespace Practice12
{
    class mainpractice
    {
        public static void Main(string[] args)
        {
            Practice obj = new Practice();
            CRUD crudobj = new CRUD();
            Console.WriteLine("-------------------------");
            Console.WriteLine("---------- MENU ---------");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1.             TABLE     ");
            Console.WriteLine("2.             CRUD      ");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Enter the Code to run :");
            int code = Convert.ToInt32(Console.ReadLine());

            switch(code)
            {
                case 1:
                    obj.Run();
                    break;
                case 2:
                    crudobj.crudmethod();
                    break;
                default:
                    Console.WriteLine("Try again");
                    break;
            }

        }
    }
}
