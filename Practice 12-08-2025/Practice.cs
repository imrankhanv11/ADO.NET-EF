using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Practice12
{
    internal class Practice
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        [Obsolete]
        public void Run()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create Table
                    //string createTableQuery = @"
                    //CREATE TABLE Employee (
                    //    Id INT PRIMARY KEY IDENTITY(1,1),
                    //    Name NVARCHAR(50),
                    //    Age INT,
                    //    Department NVARCHAR(50)
                    //)";

                    //SqlCommand command = new SqlCommand(createTableQuery, connection);
                    //command.ExecuteNonQuery();
                    //Console.WriteLine("Table created successfully.");

                    // Insert Data
                    string insertData = "Insert Into Employee (Name, Age, Department) Values ('Imran', 21, 'Software'),('Khan', 22,'Finance')";
                    SqlCommand insertCommand = new SqlCommand(insertData, connection);
                    int insertRow = insertCommand.ExecuteNonQuery();
                    Console.WriteLine("Inserted Rows :" + insertRow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
