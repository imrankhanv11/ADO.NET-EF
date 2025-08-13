using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practice13
{
    internal class DataTableclass
    {

        // input validation
        InputValidation input = new();
        public void dataTablemethod()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("--------------- Menu --------------");
            Console.WriteLine("1.                Insertdata       ");
            Console.WriteLine("2.                WithDB           ");
            Console.WriteLine("3.                Exit             ");
            Console.WriteLine("-----------------------------------");

            while (true)
            {
                Console.Write("Enter the code to run :");
                int code = Convert.ToInt32(Console.ReadLine());

                switch (code)
                {
                    case 1:
                        datatableinsert();
                        break;
                    case 2:

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

        // Create the DataTable once
        private static DataTable tbl = CreateTable();

        // Method to create the structure
        private static DataTable CreateTable()
        {

            DataTable table = new DataTable("newTable");

            // add Cloums
            // method 1
            DataColumn id = new DataColumn();
            id.ColumnName = "ID";
            id.DataType = typeof(int);
            id.Unique = true;
            id.AllowDBNull = false;
            id.AutoIncrement = true;
            id.AutoIncrementSeed = 2;
            id.AutoIncrementStep = 1;
            id.Caption = "Id";
            table.Columns.Add(id);

            // method 2
            table.Columns.Add("Name", typeof(string));

            // method 3
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Age", typeof(int)),
                new DataColumn("Department", typeof(string))
            });

            // add Rows
            // method 1
            DataRow row1 = table.NewRow();
            row1["Id"] = 1;
            row1["Name"] = "Imran";
            row1["Age"] = 21;
            row1["Department"] = "Software";
            table.Rows.Add(row1);

            DataRow newRow = table.NewRow();
            newRow["Name"] = "check";
            newRow["Age"] = 24;
            newRow["Department"] = "Finance";
            table.Rows.Add(newRow);

            DataRow newRow2 = table.NewRow();
            newRow2["Name"] = "check2";
            newRow2["Age"] = 22;
            newRow2["Department"] = "dkjfkld";
            table.Rows.Add(newRow2);

            DataRow newRow3 = table.NewRow();
            newRow3["Name"] = "check3";
            newRow3["Age"] = 4;
            newRow3["Department"] = "dkflkdjf";
            table.Rows.Add(newRow3);

            return table;
        }


        public void datatableinsert()
        {
            Console.Write("Enter the Name : ");
            string checkName = Console.ReadLine();
            string name = input.StringCheck(checkName);

            Console.Write("Enter the Age : ");
            string checkAge = Console.ReadLine();
            int age = input.IntCheck(checkAge);

            Console.Write("Enter the Department Name : ");
            string checkDepartment = Console.ReadLine();
            string Department = input.StringCheck(checkDepartment);

            // method2 
            DataRow newRow = tbl.NewRow();
            newRow["Name"] = name;
            newRow["Age"] = checkAge;
            newRow["Department"] = Department;
            tbl.Rows.Add(newRow);

            // Print header
            Console.WriteLine("ID\tName\tAge\tDepartment");
            Console.WriteLine("-----------------------------------------------");

            // Print rows
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }

        }
    }
}
