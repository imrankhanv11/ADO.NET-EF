using CodeFirstApproach.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace CodeFirstApproach.Operations
{
    public class storedProceduremethods
    {
        public void return3output()
        {
            while (true)
            {
                Console.WriteLine("1.     2output    ");
                Console.WriteLine("2.     excutesp raw  ");
                Console.WriteLine("3.     rawsql ");
                Console.WriteLine();
                Console.Write("enter the code : ");
                string code = Console.ReadLine();

                switch (code)
                {
                    case "1":
                        empDept();
                        break;
                    case "2":
                        rawsqlmethod();
                        break;
                    case "3":
                        insertvalue();
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

        public void empDept()
        {
            using (var db = new EmployeeModel())
            {
                var value = db.Database.Connection;

                value.Open();

                var cmd = value.CreateCommand();

                cmd.CommandText = "employeedepartment";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var result = cmd.ExecuteReader();

                while(result.Read())
                {
                    Console.WriteLine(result[0] + " " + result[1]);
                }

                result.NextResult();

                while (result.Read())
                {
                    Console.WriteLine(result[0] + " " + result[1]);
                }
            }
        }

        public void rawsqlmethod()
        {
            using(var db = new EmployeeModel())
            {
                var value = db.Database.SqlQuery<EmpDetails>("EXEC empdetails").ToList();

                foreach(var item in value)
                {
                    Console.WriteLine(item.Gender+" "+item.Name+" "+item.ID);
                }
            }
        }

        public void insertvalue()
        {
            using (var db = new EmployeeModel())
            {
                db.Database.ExecuteSqlCommand("insert into employees(Name, Gender, DepartmentId) values('Check','Male', 1)");

                db.SaveChanges();
            }
        }
    }
}
