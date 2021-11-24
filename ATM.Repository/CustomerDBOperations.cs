using System;
using ATM.Models;
using System.Data.SqlClient;
using System.Data;

namespace ATM.Repository
{
    public class CustomerDBOperations
    {
        public static string connectionString = @"Data Source=ANMOL\SQLEXPRESS;Initial Catalog=ATM;integrated security=SSPI";
        public void UpdateBalance(string id, double balance) 
        {
            string query = $"Update Account set Balance=N'{balance}' where id=N'{id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void InsertTransaction(string bankId, Account user, string transId, string type,double amount,Transaction trans) 
        {
            string query = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                $" VALUES(N'{transId}',N'{user.Id}',N'{user.Id}',N'{bankId}',N'{bankId}'," +
                $"N'{type}',N'{DateTime.Now}',N'{amount}')";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            Library.TransactionList.Add(trans);
            conn.Close();
        }
        public double FindExchangeRate(string currCode)
        {
            double value = 0;
            string query1 = $"select ExchangeRate from [ATM].[dbo].[Currency] where CurrencyCode=N'{currCode}'";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var CurrencyData = (IDataReader)reader;
                string v = Convert.ToString(CurrencyData[0]);
                value = Convert.ToDouble(v);
            }
            reader.Close();
            conn.Close();
            return value;
        }

    }
}
