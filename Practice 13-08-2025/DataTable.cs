using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
            Console.WriteLine("2.                Copy & Clone     ");
            Console.WriteLine("3.                Merge            ");
            Console.WriteLine("4.                Delete & Remove  ");
            Console.WriteLine("5.                With DB          ");
            Console.WriteLine("6.                Exit             ");
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
                        CopyandClone();
                        break;
                    case 3:
                        merger();
                        break;
                    case 4:
                        deleteandRemove();
                        break;
                    case 5:

                        break;
                    case 6:
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
            Console.WriteLine("ID\tName\t\tAge\tDepartment");
            Console.WriteLine("-----------------------------------------------");

            // Print rows
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();
        }

        public void CopyandClone()
        {
            // for clone
            DataTable clonetable = new DataTable();

            // for copy table 
            DataTable copytable = new DataTable();

            clonetable = tbl.Clone();

            copytable = tbl.Copy();

            Console.WriteLine("This is clone table :");
            foreach(DataColumn col in clonetable.Columns)
            {
                Console.WriteLine(col.ColumnName+" "+col.DataType);
            }
            Console.WriteLine();
            Console.WriteLine("This is Copy table : ");
            foreach(DataRow row in copytable.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();

            // copy rows form one table to another
            Console.WriteLine("Import from another table rows : ");

            clonetable.ImportRow(copytable.Rows[0]);
            clonetable.ImportRow(copytable.Rows[1]);
            foreach (DataRow row in clonetable.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
        }

        public void merger()
        {
            DataTable newtable = new DataTable();


            // method 3
            newtable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Age", typeof(int)),
                new DataColumn("Department", typeof(string))
            });

            DataRow row1 = newtable.NewRow();
            row1["ID"] = 99;
            row1["Name"] = "Imran";
            row1["Age"] = 21;
            row1["Department"] = "Software";
            newtable.Rows.Add(row1);

            newtable.Merge(tbl, true);

            foreach (DataRow row in newtable.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
        }

        public void deleteandRemove()
        {
            Console.WriteLine("Before Delete : ");
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();

            foreach(DataRow row in tbl.Rows)
            {
                if (Convert.ToInt32(row[0]) == 2)
                {
                    row.Delete();
                }
            }
            tbl.AcceptChanges(); // accept

            Console.WriteLine("After Delete : ");
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();

            foreach (DataRow row in tbl.Rows)
            {
                if (Convert.ToInt32(row[0]) == 3)
                {
                    row.Delete();
                }
            }

            tbl.RejectChanges(); // reject

            Console.WriteLine("After Delete (but reject it) : ");
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();

            // remove
            // Remove rows where ID = 3 using Remove()
            DataRow[] rowsToRemove = tbl.Select("Id = 3");

            foreach (DataRow row in rowsToRemove)
            {
                tbl.Rows.Remove(row);
            }

            //for (int i = tbl.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (Convert.ToInt32(tbl.Rows[i]["Id"]) == 3)
            //    {
            //        tbl.Rows.Remove(tbl.Rows[i]);
            //    }
            //}

            Console.WriteLine("After Remove : ");
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine($"{row["ID"],-5} {row["Name"],-15} {row["Age"],-5} {row["Department"],-10}");
            }
            Console.WriteLine();
        }
    }
}
