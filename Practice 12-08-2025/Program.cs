using System;
using System.Data.SqlClient;

namespace Practice12
{
    class mainpractice
    {
        public static void Main(string[] args)
        {
            //for basic
            Practice obj = new Practice();
            // for Crud
            CRUD crudobj = new ();
            // for SP
            Sp sp = new Sp();

            // 1---> Basic
            // 2---> CRUD with inputvalidation and dynamic changes (With input Parameter)
            // 3---> SP with input and output parameters
            Console.WriteLine("-------------------------");
            Console.WriteLine("---------- MENU ---------");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1.             C-TABLE   ");
            Console.WriteLine("2.             CRUD      ");
            Console.WriteLine("3.             SP        ");
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
                case 3:
                    sp.showSPmethods();
                    break;
                default:
                    Console.WriteLine("Try again");
                    break;
            }

        }
    }
}
