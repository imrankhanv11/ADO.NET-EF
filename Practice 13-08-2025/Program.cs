using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice13
{
    class mainone
    {
        public static void Main(string[] args)
        {

            DataTableclass onevl = new();

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("--------------- Menu --------------");
            Console.WriteLine("1.                DataTable        ");
            Console.WriteLine("2.                DataSet          ");
            Console.WriteLine("3.                Exit             ");
            Console.WriteLine("-----------------------------------");

            while (true)
            {
                Console.Write("Enter the code to run :");
                int code = Convert.ToInt32(Console.ReadLine());

                switch(code)
                {
                    case 1:
                        onevl.dataTablemethod();
                        break;
                    case 2:
                        DataSetvalue.DataSetMethod();
                        break;
                    case 3:
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