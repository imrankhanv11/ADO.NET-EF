using DbFirst_EFCore_01;
using DBFirst_EFCore_01.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirst_EFCore_01.Operations
{
    public class sp_operations
    {
        InputValidation input = new InputValidation();
        public void basicselect()
        {
            using(var db = new StudentContext())
            {
                var output = db.EmployeeDepartments.FromSqlRaw("exec sp_departmentemployee").ToList();

                foreach (var item in output)
                {
                    Console.WriteLine(item.DepartmentName+" "+item.Gender+" "+item.EmployeeName);
                }
            }
        }

        public void basicselect2()
        {
            using( var db = new StudentContext())
            {
                var output = db.EmployeeCounts.FromSqlRaw("exec sp_employeeCount").ToList();
                
                if (output == null || output.Count == 0)
                {
                    Console.WriteLine("NO data found");
                }
                else
                {
                    foreach (var item in output)
                    {
                        Console.WriteLine(item.TotalEmp);
                    }

                }
            }
        }

        public void departmentEmpID()
        {
            Console.Write("Enter the Department ID : ");
            string checkid = Console.ReadLine();
            int id = input.IntCheck(checkid);

            SqlParameter idpar = new SqlParameter("@id", id);

            using( var db = new StudentContext())
            {
                var output = db.EmpWithDepartments.FromSqlRaw("EXEC sp_departmentEmployeeID @id", idpar).ToList();

                if (output == null || output.Count == 0)
                {
                    Console.WriteLine("NO data found");
                }
                else
                {
                    foreach (var item in output)
                    {
                        Console.WriteLine(item.EmployeeName + " " + item.Gender);
                    }

                }
                
            }
        }

        public void projectcount()
        {
            using(var db = new StudentContext())
            {
                var outptut = new SqlParameter
                {
                    ParameterName = "@count",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var output = db.Database.ExecuteSqlRaw("exec sp_projectCount @count output", outptut);

                var result = (int)outptut.Value;

                Console.WriteLine(result);
            }
        }

        public void insertDepartment()
        {
            Console.Write("Enter the Department Name : ");
            string checkName = Console.ReadLine();
            string DepartmentName = input.StringCheck(checkName);

            SqlParameter name = new SqlParameter("@name", DepartmentName);

            Console.Write("Enter the Block : ");
            string checkBlock = Console.ReadLine();
            string Block = input.StringCheck(checkBlock);

            SqlParameter location = new SqlParameter("@location", Block);

            SqlParameter id = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            using(var db = new StudentContext())
            {
                db.Database.ExecuteSqlRaw("exec sp_insertDepartment @name, @location, @id out", name, location, id);

                var result = (int)id.Value;

                Console.WriteLine(result);
            }


        }
    }
}
