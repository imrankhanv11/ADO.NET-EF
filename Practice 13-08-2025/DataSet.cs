using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice13
{
    internal class DataSetvalue
    {
        public static void DataSetMethod()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Students; SELECT * FROM Employee;";

                conn.Open();

                SqlTransaction sqlTransaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn, sqlTransaction))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();

                        adapter.Fill(ds);

                        Console.WriteLine("Students Table:");
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"{row["ID"]} - {row["Name"]}");
                        }

                        Console.WriteLine("\nEmployee Table:");
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            Console.WriteLine($"{row["ID"]} - {row["Name"]}");
                        }
                    }

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("error");
                    }

                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

    }
}
