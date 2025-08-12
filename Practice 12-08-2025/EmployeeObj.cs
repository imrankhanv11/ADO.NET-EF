using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice12
{
    internal class EmployeeObj
    {
        private Dictionary<int, Employees> Employee = new Dictionary<int, Employees>();
        int count = 1;


        string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";

        public void addEmployee()
        {
            try
            {
                using (SqlConnection employeedata = new SqlConnection(connectionString))
                {
                    employeedata.Open();
                    SqlCommand cmd = employeedata.CreateCommand();
                    cmd.CommandText = "sp_all";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Employee.Add(count++, new Employees( Convert.ToInt32(dr["ID"]),dr["Name"].ToString(),Convert.ToInt32(dr["Age"]),dr["Department"].ToString()
));
                    }
                }
                foreach (var kvp in Employee)
                {
                    Console.WriteLine($"Key: {kvp.Key}");
                    Console.WriteLine($"ID: {kvp.Value.Id}");
                    Console.WriteLine($"Name: {kvp.Value.Name}");
                    Console.WriteLine($"Age: {kvp.Value.Age}");
                    Console.WriteLine($"Department: {kvp.Value.Department}");
                    Console.WriteLine("-----------------------------------");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}
