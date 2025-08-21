using DbFirst_EFCore_01;
using DBFirst_EFCore_01.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirst_EFCore_01.Operations
{
    public class insertBulk
    {
        InputValidation input = new InputValidation();
        public void insertemp()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("DepartmentID", typeof(int));

            string check;
            do
            {
                Console.Write("Enter the Name : ");
                string checkName = Console.ReadLine();
                string Name = input.StringCheck(checkName);

                Console.Write("Enter the Gender : ");
                string checkGender = Console.ReadLine();
                string Gender = input.StringCheck(checkGender);


                Dictionary<int, int> list = new Dictionary<int, int>();
                Console.WriteLine("Department List :");
                using(var db = new StudentContext())
                {
                    var dpt = db.Departments.ToList();
                    int count = 1;
                    foreach (var item in dpt)
                    {
                        list.Add(count, item.DepartmentId);
                        Console.WriteLine($"{count++} : {item.DepartmentName}");
                    }
                }

                Console.Write("Enter the Code of Department : ");
                string checkCode = Console.ReadLine();
                int code = input.IntCheck(checkCode);

                int dptcode = list[code];

                dataTable.Rows.Add(Name, Gender, dptcode);

                Console.Write("Do you want to continue (yes/no) : ");
                check = Console.ReadLine().Trim().ToLower();
            } while (check == "yes");

            SqlParameter emp = new SqlParameter("@Employees", dataTable);
            emp.SqlDbType = SqlDbType.Structured;
            emp.TypeName = "empinsert";

            using (var db = new StudentContext())
            {
                db.Database.ExecuteSqlRaw("EXEC InsertEmployees @Employees", emp);
            }
        }
    }
}
