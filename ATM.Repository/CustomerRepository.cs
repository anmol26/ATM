using System;
using ATM.Models;
using System.Data.SqlClient;
using System.Data;

namespace ATM.Repository
{
    public class CustomerRepository
    {
        readonly Library lib = new Library();
        public void UpdateBalance(string id, double balance)
        {
            try
            {
                string query = $"Update Account set Balance=N'{balance}' where id=N'{id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertTransaction(string transId, string type, double amount, Transaction trans)
        {
            try
            {
                string query = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                    $" VALUES(N'{transId}',N'{trans.SenderAccountId}',N'{trans.RecieverAccountId}',N'{trans.SenderBankId}',N'{trans.RecieverBankId}'," +
                    $"N'{type}',N'{DateTime.Now}',N'{amount}')";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
                lib.GetTransactionList().Add(trans);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public double FindExchangeRate(string currCode)
        {
            double value = 0;
            try
            {
                string query1 = $"select ExchangeRate from [ATM].[dbo].[Currency] where CurrencyCode=N'{currCode}'";
                SqlCommand command = new SqlCommand(query1, DatabaseConnection.ConnectDatabase());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var CurrencyData = (IDataReader)reader;
                    string v = Convert.ToString(CurrencyData[0]);
                    value = Convert.ToDouble(v);
                }
                reader.Close();
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
