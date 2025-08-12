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

            // 1---> Basic
            // 2---> CRUD with inputvalidation and dynamic changes
            Console.WriteLine("-------------------------");
            Console.WriteLine("---------- MENU ---------");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1.             C-TABLE   ");
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
