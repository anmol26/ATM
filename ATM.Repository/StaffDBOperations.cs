using System;
using ATM.Models;
using System.Data.SqlClient;

namespace ATM.Repository
{
    public class StaffDBOperations
    {
        public static string connectionString = @"Data Source=ANMOL\SQLEXPRESS;Initial Catalog=ATM;integrated security=SSPI";
        public void InsertNewBank(string bankId,Bank bank, string name,string address, string branch, string currencyCode) 
        {
            Library.BankList.Add(bank);
            string query = $"INSERT INTO Bank" +
                $" VALUES(N'{bankId}',N'{name}',N'{address}',N'{branch}',N'{currencyCode}'," +
                $"N'{bank.SameRTGS}',N'{bank.SameIMPS}',N'{bank.DiffRTGS}',N'{bank.DiffIMPS}')";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void InsertNewStaff(Staff s,string staffId, string name,string password, long phoneNumber,string gender,string bankId ) 
        {
            Library.StaffList.Add(s);
            string query = $"INSERT INTO Staff" +
           $" VALUES(N'{staffId}',N'{name}',N'{password}',N'{phoneNumber}',1," +
           $"N'{DateTime.Now}',N'{gender}',N'{bankId}')";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void InsertNewAccount(Account a, string accountId,string name, string password,long phoneNumber,string gender,string bankId)
        {
            Library.AccountList.Add(a);
            string query = $"INSERT INTO Account" +
           $" VALUES(N'{accountId}',N'{name}',N'{password}',N'{phoneNumber}',0,1," +
           $"N'{gender}',N'{DateTime.Now}',N'{bankId}')";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteAccount(Account user) 
        {
            Library.AccountList.Remove(user);
            string query = $"Delete from Account where id=N'{user.Id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void InsertNewCurrency(string code, double rate)
        {
            string query = $"Insert into Currency Values(N'{code}',N'{rate}') ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateCharges(string bankId, double rtgs, double imps, int choice)
        {
            if (choice == 1)
            {
                string query1 = $"Update Bank set SameRTGS=N'{rtgs}', SameIMPS=N'{imps}' where id=N'{bankId}' ";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand(query1, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            else if (choice == 2)
            {
                string query2 = $"Update Bank set DiffRTGS=N'{rtgs}', DiffIMPS=N'{imps}' where id=N'{bankId}' ";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand command = new SqlCommand(query2, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateBalance(string id, double balance)
        {
            string query = $"Update Account set Balance=N'{balance}' where id=N'{id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateName(Account bankAccount, string name)
        {
            string query = $"Update Account set Name=N'{name}' where id=N'{bankAccount.Id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdatePhoneNumber(Account bankAccount, long number)
        {
            string query = $"Update Account set PhoneNumber=N'{number}' where id=N'{bankAccount.Id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdatePassword(Account bankAccount, string password)
        {
            string query = $"Update Account set Password=N'{password}' where id=N'{bankAccount.Id}' ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
