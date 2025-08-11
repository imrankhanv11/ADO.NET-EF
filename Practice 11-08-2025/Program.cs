using System;
using System.Data;
using System.Data.SqlClient;
class Practice
{
    static void Main()
    {
        string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Using ExcuteReader");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ID"]} - {reader["Name"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error: " + ex.Message);
            }
        }
        Console.WriteLine("-----------------------------------------------------");
        using (SqlConnection conn2 = new SqlConnection(connectionString))
        {
            try
            {
                conn2.Open();
                // --------- READ ---------------------
                string query = "SELECT SUM(Age) FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn2);
                object totalage = cmd.ExecuteScalar();
                Console.WriteLine("Using ExcuteScalar");
                Console.WriteLine("The Total Age :" + totalage);

                // Excutenonscalar
                //------------ INSERT --------------------
                string queryinsert = "INSERT INTO Students (Name, Age, Course) VALUES ('Imran Khan', 22, 'B.Tech AI & DS'), ('John Smith', 23, 'BCA')";
                SqlCommand cmd2 = new SqlCommand(queryinsert, conn2);
                int affectedrow = cmd2.ExecuteNonQuery();
                Console.WriteLine(affectedrow);

                // ------------ DELETE --------------------
                string deletequery = "Delete Students where id=5";
                SqlCommand cmd3 = new SqlCommand(deletequery, conn2);
                int deletedRows = cmd3.ExecuteNonQuery();
                Console.WriteLine(deletedRows);

                //--------- UPDATE ------------------------
                string update = "UPDATE Students SET Name = 'Update' WHERE Id = 2";
                SqlCommand cmd4 = new SqlCommand(update, conn2);
                int updaterows = cmd4.ExecuteNonQuery();
                Console.WriteLine(updaterows);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        using (SqlConnection newcon = new  SqlConnection(connectionString))
        {
            try
            {
                newcon.Open();
                Console.Write("Enter the Id to Delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                string sqldelteid = "DELETE FROM Students WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sqldelteid, newcon);
                cmd.Parameters.AddWithValue("@Id", id);

                int Delterows = cmd.ExecuteNonQuery();
                Console.WriteLine(Delterows + " row(s) deleted.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error :" + ex.Message);
            }
        }
    }
}
