using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ATM.Repository
{
    public class DatabaseConnection
    {
        const string connectionString = @"Data Source=ANMOL\SQLEXPRESS;Initial Catalog=ATM;integrated security=SSPI";

        public static SqlConnection conn;
        public static SqlConnection ConnectDatabase()
        {
            conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return conn;
        }
    }
}
