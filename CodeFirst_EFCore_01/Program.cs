using CodeFirst_EFCore_01.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeFirst_EFCore_01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter the code : ");
                string code = Console.ReadLine();

                switch(code)
                {
                    case "1":
                        sp();
                        break;
                    case "2":
                        spoutput();
                        return;
                    default:
                        break;
                }
            }
        }

        public static void sp()
        {
            using(var db = new EmployeeContext())
            {
                int deptId = 1;
                var deptIdParam = new SqlParameter("@DepartmentId", deptId);

                var employees = db.spEmployee
                    .FromSqlRaw("EXEC GetEmployeesByDepartment @DepartmentId", deptIdParam)
                    .ToList();

                foreach (var emp in employees)
                {
                    Console.WriteLine(emp.Name);
                }
            }
        }

        public static void spoutput()
        {
            using (var db = new EmployeeContext())
            {
                var countParam = new SqlParameter
                {
                    ParameterName = "@Output",  
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                db.Database.ExecuteSqlRaw("EXEC GetCountEmployee @Output OUTPUT", countParam);

                int output = (int)countParam.Value;

                Console.WriteLine("Total Employees: " + output);
            }
        }

    }
}