using CodeFirstApproach.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApproach.Operations
{
    internal class Crud
    {
        public void crudoperaion()
        {
            while (true)
            {
                Console.WriteLine("1.     Read    ");
                Console.WriteLine("2.     Insert  ");
                Console.WriteLine("3.     Update  ");
                Console.WriteLine("4.     Delete  ");
                Console.WriteLine();
                Console.Write("enter the code : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        read();
                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    case "4":

                        break;
                    case "5":
                        Console.WriteLine("Thank you");
                        return;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }
        }

        public void read()
        {
            using (var dbcontext = new EmployeeModel())
            {
                var student = dbcontext.Employee.Select(e => new EmployeeDto { 
                        Name = e.Name,
                        Gender = e.Gender
                });

                foreach(var emp in student)
                {
                    Console.WriteLine(emp.Name + " " + emp.Gender);
                }

            }
        }
    }
}
