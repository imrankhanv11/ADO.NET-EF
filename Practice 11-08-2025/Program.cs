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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }
    }
}
