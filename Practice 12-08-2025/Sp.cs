using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice12
{
    internal class Sp
    {
        InputValidation input = new InputValidation();

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void showSPmethods()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("------------- MENU ------------");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1.    SP with Input Prameter   ");
            Console.WriteLine("2.    SP with Output Prameter  ");
            Console.WriteLine("3.    SP with Output Rows(1)   ");
            Console.WriteLine("4.    SP with Output Rows(n)   ");
            Console.WriteLine("5.    EXIT                     ");
            Console.WriteLine("-------------------------------");
            
            while (true)
            {

                Console.WriteLine("Enter the Code to run :");
                int code = Convert.ToInt32(Console.ReadLine());

                switch (code)
                {
                    case 1:
                        InputPrameter();
                        break;
                    case 2:
                        OutputPrameter();
                        break;
                    case 3:
                        Outputrows1();
                        break;
                    case 4:
                        Outputrowsn();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }
        }

        public void InputPrameter()
        {

            try
            {
                Console.WriteLine("Enter the input id : ");
                int id = input.IntCheck(Console.ReadLine());

                using (SqlConnection inputpramcon = new SqlConnection(connectionString))
                {
                    inputpramcon.Open();
                    SqlCommand inputCmd = new SqlCommand("sp_input", inputpramcon);
                    inputCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    inputCmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = inputCmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        //string col1 = reader.IsDBNull(0) ? "NULL" : reader[0].ToString();
                        //string col2 = reader.IsDBNull(1) ? "NULL" : reader[1].ToString();

                        Console.WriteLine(reader[0] + " " + reader[1]);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void OutputPrameter()
        {
            try
            {
                Console.WriteLine("Enter the input id : ");
                int id = input.IntCheck(Console.ReadLine());

                using (SqlConnection outputpramConnection = new SqlConnection(connectionString))
                {
                    outputpramConnection.Open();
                    SqlCommand outputCmd = new SqlCommand("sp_output", outputpramConnection);

                    outputCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    outputCmd.Parameters.AddWithValue("@id", id);

                    outputCmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50).Direction = System.Data.ParameterDirection.Output;

                    outputCmd.ExecuteNonQuery();

                    string output = outputCmd.Parameters["@name"].Value.ToString();

                    Console.WriteLine($"The name is {output}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void Outputrows1()
        {
            try
            {
                using(SqlConnection outputconnetion = new SqlConnection (connectionString))
                {
                    outputconnetion.Open();
                    SqlCommand outputCmd = outputconnetion.CreateCommand();
                    outputCmd.CommandText = "sp_all";
                    outputCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = outputCmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Console.WriteLine($"ID : {reader[0]}, Name : {reader["Name"]}");
                    }
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void Outputrowsn()
        {
            try
            {
                using (SqlConnection outputncon = new SqlConnection(connectionString))
                {
                    outputncon.Open();
                    SqlCommand outputCmd = new SqlCommand("sp_n", outputncon);
                    outputCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader result = outputCmd.ExecuteReader();

                    Console.WriteLine("Employee Table :");
                    while(result.Read())
                    {
                        Console.WriteLine($"ID : {result[0]}, Name : {result["Name"]}");
                    }
                    Console.WriteLine();

                    while (result.NextResult())
                    {
                        Console.WriteLine("Student Table:");
                        while (result.Read())
                        {
                            Console.WriteLine($"ID : {result[0]}, Name : {result["Name"]}");
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}
