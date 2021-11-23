using System;
using ATM.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace ATM.Services
{
    public class Library
    {
        public Library()
        {
            ConnectDatabase();
            GetBanks();
            GetStaff();
            GetAccount();
            GetTransaction();
            GetCurrency();
        }

        public static List<Bank> BankList = new List<Bank>();
        public static List<Staff> StaffList = new List<Staff>();
        public static List<Account> AccountList = new List<Account>();
        public static List<Transaction> TransactionList = new List<Transaction>();
        public static List<Currency> CurrencyList = new List<Currency>();

        private SqlConnection conn;
        public SqlConnection ConnectDatabase()
        {
            Console.WriteLine("Getting Connection to Database...");
            conn = new SqlConnection(@"Data Source=ANMOL\SQLEXPRESS;Initial Catalog=ATM;integrated security=SSPI");
            try
            {
                Console.WriteLine("Openning Database Connection...");
                conn.Open();
                Console.WriteLine("Database Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return conn;
        }
        public void GetBanks()
        {
            string query = "SELECT * FROM Bank";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var bankData = (IDataReader)reader;
                Bank bank = new Bank(Convert.ToString(bankData[1]), Convert.ToString(bankData[2]), Convert.ToString(bankData[3]), Convert.ToString(bankData[4]), Convert.ToString(bankData[0]));
                BankList.Add(bank);
            }
            reader.Close();
        }
        public void GetStaff()
        {
            string query = "SELECT * FROM Staff";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var staffData = (IDataReader)reader;
                string num = Convert.ToString(staffData[3]);
                long number = long.Parse(num);
                Staff staff = new Staff(Convert.ToString(staffData[7]), Convert.ToString(staffData[1]), number, Convert.ToString(staffData[2]), Convert.ToString(staffData[6]), Convert.ToString(staffData[0]));
                StaffList.Add(staff);
            }
            reader.Close();
        }
        public void GetAccount()
        {
            string query = "SELECT * FROM Account";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var AccountData = (IDataReader)reader;
                string num = Convert.ToString(AccountData[3]);
                long number = long.Parse(num);
                double balance= Convert.ToDouble(AccountData[4]);
                Account account = new Account(Convert.ToString(AccountData[8]), Convert.ToString(AccountData[1]),number, Convert.ToString(AccountData[2]), Convert.ToString(AccountData[6]), Convert.ToString(AccountData[0]),balance);
                AccountList.Add(account);
            }
            reader.Close();
        }
        public void GetTransaction()
        {
            string query = "SELECT * FROM [ATM].[dbo].[Transaction]";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var transactionData = (IDataReader)reader;
                string amt = Convert.ToString(transactionData[7]);
                double amount = double.Parse(amt);
                string t= Convert.ToString(transactionData[5]);
                int type;
                if (t == "Credit") { type = 1; }
                else { type = 2; }
                Transaction trans = new Transaction( amount, type, Convert.ToString(transactionData[1]), Convert.ToString(transactionData[2]),Convert.ToString(transactionData[3]), Convert.ToString(transactionData[4]), Convert.ToString(transactionData[0]));
                TransactionList.Add(trans);
            }
            reader.Close();
        }
        public void GetCurrency()
        {
            string query = "SELECT * FROM [ATM].[dbo].[Currency]";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var CurrencyData = (IDataReader)reader;
                string er = Convert.ToString(CurrencyData[2]);
                double exchangerate = Convert.ToDouble(er);
                Currency currency = new Currency(Convert.ToString(CurrencyData[1]), exchangerate);
                CurrencyList.Add(currency);
            }
            reader.Close();
        }

    }
}
