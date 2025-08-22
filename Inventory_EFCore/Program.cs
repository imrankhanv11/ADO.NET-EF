using DBFirst_EFCore_01.Models;
using DBFirst_EFCore_01.Operations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DBFirst_EFCore_01
{
    public class Program
    {
        public static void Main(string[] args)
        {

            sp_operations sp = new sp_operations();

            insertBulk insert = new insertBulk();
            while (true)
            {
                //select
                Console.WriteLine("1.      departmentEmp");
                //select as count
                Console.WriteLine("2.      departmentEmpID");
                //input
                Console.WriteLine("3.      employeeCount");
                //output
                Console.WriteLine("4.      projectCount");
                //insertdepartment
                Console.WriteLine("5.      InsertDepartment");
                //list of data via dataTable --> sp ( UDTT )
                Console.WriteLine("6       inserBulkEmp");
                //exit
                Console.WriteLine("7.      Exit");

                Console.Write("Enter the code : ");
                string code = Console.ReadLine();

                switch (code) {
                    case "1":
                        sp.basicselect();
                        break;
                    case "2":
                        sp.basicselect2();
                        break;
                    case "3":
                        sp.departmentEmpID();
                        break;
                    case "4":
                        sp.projectcount();
                        break;
                    case "5":
                        sp.insertDepartment();
                        break;
                    case "6":
                        insert.insertemp(); 
                        break;
                    case "7":
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("try agian");
                        break;
                }
            }
        }
    }
}