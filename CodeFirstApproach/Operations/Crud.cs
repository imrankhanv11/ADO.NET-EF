using CodeFirstApproach.DTO;
using CodeFirstApproach.Models;
using CodeFirstApproach.Validation;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApproach.Operations
{
    internal class Crud
    {
        InputValidations input = new InputValidations();
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
                        insert();
                        break;
                    case "3":

                        break;
                    case "4":
                        delete();
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
                IEnumerable<EmployeeDto> student = dbcontext.Employee.Select(e => new EmployeeDto{ 
                        Name = e.Name,
                        Gender = e.Gender
                });

                foreach(var emp in student)
                {
                    Console.WriteLine(emp.Name + " " + emp.Gender);
                }


                // Dapper
                var connection = dbcontext.Database.Connection;

                connection.Open();

                string sql = @"
                            SELECT e.Name, e.Gender, d.DepartmentName
                            FROM Employees e
                            JOIN Departments d ON d.DepartmentId = e.DepartmentId";

                var result = connection.Query<dapperEmployee>(sql).ToList();

                foreach (var emp in result)
                {
                    Console.WriteLine($"Name: {emp.Name}, Gender: {emp.Gender}, Department: {emp.DepartmentName}");
                }
            }

        }

        public void delete()
        {
            
        }

        public void insert()
        {
            //Console.Write("Enter the Department Name :");
            //string checkDepartment = Console.ReadLine();
            //string Department = input.StringCheck(checkDepartment);

            //Console.Write("Enter the Location : ");
            //string checkLocation = Console.ReadLine();
            //string Location = input.StringCheck(checkLocation);

            //Console.Write("Enter the Employee Name :");
            //string checkName = Console.ReadLine();
            //string Name = input.StringCheck(checkName);

            //Console.Write("Enter the Gender : ");
            //string checkGender = Console.ReadLine();
            //string Gender = input.StringCheck(checkGender);


            //DeptEmp values = new DeptEmp
            //{
            //    DepartmentName = Department,
            //    Location = Location,
            //    Name = Name,
            //    Gender = Gender
            //};

            //using (var dbcontext = new EmployeeModel())
            //{
            //    var emp = new Employees
            //    {
            //        Name = values.Name,
            //        Gender = values.Gender,
            //        Department = new Department
            //        {
            //            DepartmentName = values.DepartmentName,
            //            Location = values.Location
            //        }
            //    };

            //    dbcontext.Employee.Add(emp);

            //    dbcontext.SaveChanges();
            //}

            
            //    Console.WriteLine("Succesfull");

            //    Console.WriteLine("-----------------------------------");


            Console.Write("Enter the Department Name : ");
            string checkDepartment2 = Console.ReadLine();
            string Department2 = input.StringCheck(checkDepartment2);

            Console.Write("Enter the Location : ");
            string checkLocation2 = Console.ReadLine();
            string Location2 = input.StringCheck(checkLocation2);

            Console.Write("Enter the Employee Name : ");
            string checkName2 = Console.ReadLine();
            string Name2 = input.StringCheck(checkName2);

            Console.Write("Enter the Gender : ");
            string checkGender2 = Console.ReadLine();
            string Gender2 = input.StringCheck(checkGender2);

            Console.Write("Enter the Project Name : ");
            string checkProject2 = Console.ReadLine();
            string Project2 = input.StringCheck(checkProject2);

            Console.Write("Enter the Budget : ");
            string checkBudget2 = Console.ReadLine();
            int Budget2 = input.IntCheck(checkBudget2);

            Console.Write("Enter the Roll : ");
            string checkRoll = Console.ReadLine();
            string Roll = input.StringCheck(checkRoll);



            using(var dbcontext = new EmployeeModel())
            {
                EmployeeProject newone = new EmployeeProject
                {
                    Roll = Roll,

                    Employees = new Employees
                    {
                        Name = Name2,
                        Gender = Gender2,

                        Department = new Department
                        {
                            DepartmentName = Department2,
                            Locations = Location2
                        }
                    },
                    Projects = new Projects
                    {
                        Budget = Budget2,
                        ProjectName = Project2
                    }
                };

                dbcontext.EmployeesProjects.Add(newone);
                dbcontext.SaveChanges();
            }

        }
    }
}
