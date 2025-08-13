using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Practice12
{
    internal class CRUD
    {
        Operations oprObj = new Operations();

        public void crudmethod() {
            Console.WriteLine("-------------------------");
            Console.WriteLine("---------- CRUD ---------");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1.             INSERT    ");
            Console.WriteLine("2.             UPDATE    ");
            Console.WriteLine("3.             READ      ");
            Console.WriteLine("4.             DELETE    ");
            Console.WriteLine("5.             EXIT      ");
            Console.WriteLine("-------------------------");
            
            while (true)
            {

                Console.WriteLine("Enter the Code to do Operations : ");
                int code = Convert.ToInt32(Console.ReadLine());

                switch (code)
                {
                    case 1:
                        oprObj.insertrecode();
                        break;
                    case 2:
                        oprObj.updaterecode();
                        break;
                    case 3:
                        oprObj.readrecode();
                        break;
                    case 4:
                        oprObj.deleterecode();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Enter the Correct code");
                        break;
                }
            }
        }
    }

    class Operations
    {

        InputValidation input = new InputValidation();

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public void insertrecode()
        {
            Console.WriteLine("Enter the name to insert : ");
            string insertName = input.StringCheck(Console.ReadLine());

            Console.WriteLine("Enter the Age to insert : ");
            int insertAge = input.IntCheck(Console.ReadLine());

            Console.WriteLine("Enter The Department to insert : ");
            string insertDepartment = input.StringCheck(Console.ReadLine());


            using (SqlConnection insertcon = new SqlConnection(connectionString))
            {
                insertcon.Open();
                string insertquery = "Insert Into Employee (Name, Age, Department) Values (@name, @age, @department)";
                SqlCommand insertCmd = new SqlCommand(insertquery, insertcon);

                //metod 1 add parameter using sqlparameter
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@name";
                param1.Value = insertName;
                param1.SqlDbType = System.Data.SqlDbType.VarChar;
                param1.Size = 50;

                insertCmd.Parameters.Add(param1);

                //method 2 add parameter using add()
                insertCmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = insertAge;

                //method 3 add parameter using addWithValue()
                insertCmd.Parameters.AddWithValue("@department", insertDepartment);

                int result = insertCmd.ExecuteNonQuery();
                Console.WriteLine($"Inserted Rows : {result}");

            }
            readrecode();
        }

        public void deleterecode()
        {
            Console.Write("Enter the id to delete : ");
            int deleteId = input.IntCheck(Console.ReadLine());

            using(SqlConnection deleteconnect = new SqlConnection(connectionString))
            {
                deleteconnect.Open();

                string delteQuery = "DELETE FROM Employee WHERE id=@id";

                SqlCommand delcmd = new SqlCommand(delteQuery, deleteconnect);
                delcmd.Parameters.AddWithValue("@id", deleteId);

                int result = delcmd.ExecuteNonQuery();
                Console.WriteLine($"Deleted Rows : {result}");
            }
            readrecode();

        }

        public void updaterecode()
        {
            Console.Write("Enter the id to upadte : ");
            int updateId = input.IntCheck(Console.ReadLine());
            Console.WriteLine("Enter the age to Update : ");
            int upage = input.IntCheck(Console.ReadLine());

            //Console.WriteLine("------------------------------");
            //Console.WriteLine("1.             NAME           ");
            //Console.WriteLine("2.             AGE            ");
            //Console.WriteLine("3.             DEPARMENT      ");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine("Enter the Propery to update : ");
            //int code = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection updateconnection = new SqlConnection(connectionString))
            {
                updateconnection.Open();
                string upquery = "UPDATE Employee SET AGE=@age WHERE id=@id";
                SqlCommand upCmd = updateconnection.CreateCommand();
                upCmd.CommandText = upquery;
                upCmd.Parameters.AddWithValue("@age", upage);
                upCmd.Parameters.AddWithValue("@id", updateId);

                int result = upCmd.ExecuteNonQuery();
                Console.WriteLine($"Affter rows : ${result}");
            }
            readrecode();
        }

        public void readrecode()
        {
            using(SqlConnection readconnetion = new SqlConnection(connectionString))
            {
                readconnetion.Open();
                string readquery = "SELECT * From Employee";
                SqlCommand readCmd= readconnetion.CreateCommand();
                readCmd.CommandText = readquery;

                SqlDataReader reader = readCmd.ExecuteReader();
                Console.WriteLine();
                while(reader.Read())
                {
                    Console.WriteLine($"ID : {reader[0]}, Name : {reader["Name"]}, Age : {reader[2]}, Department : {reader[3]}");
                    Console.WriteLine();
                }
            }
        }
    }
}
