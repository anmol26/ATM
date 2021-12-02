//using System;
//using ATM.Models;
//using System.Data.SqlClient;
//using System.Collections.Generic;
//using System.Data;

//namespace ATM.Repository
//{
//    public class Library
//    {
//        public List<Bank> GetBankList()
//        {
//            List<Bank> BankList = new List<Bank>();
//            string query = "SELECT * FROM Bank";
//            SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                var bankData = (IDataReader)reader;
//                Bank bank = new Bank(Convert.ToString(bankData["Name"]), Convert.ToString(bankData["Address"]), Convert.ToString(bankData["Branch"]), Convert.ToString(bankData["Currency"]), Convert.ToString(bankData["id"]));
//                BankList.Add(bank);
//            }
//            reader.Close();
//            return BankList;
//        }

//        public List<Staff> GetStaffList()
//        {
//            List<Staff> StaffList = new List<Staff>();
//            string query = "SELECT * FROM Staff";
//            SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                var staffData = (IDataReader)reader;
//                string num = Convert.ToString(staffData["PhoneNumber"]);
//                long number = long.Parse(num);
//                Staff staff = new Staff(Convert.ToString(staffData["BankId"]), Convert.ToString(staffData["Name"]), number, Convert.ToString(staffData["Password"]), Convert.ToString(staffData["Gender"]), Convert.ToString(staffData["id"]));
//                StaffList.Add(staff);
//            }
//            reader.Close();
//            return StaffList;
//        }
//        public List<Account> GetAccountHolderList()
//        {
//            List<Account> AccountList = new List<Account>();
//            string query = "SELECT * FROM Account";
//            SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                var AccountData = (IDataReader)reader;
//                string num = Convert.ToString(AccountData["PhoneNumber"]);
//                long number = long.Parse(num);
//                double balance = Convert.ToDouble(AccountData["Balance"]);
//                Account account = new Account(Convert.ToString(AccountData["BankId"]), Convert.ToString(AccountData["Name"]), number, Convert.ToString(AccountData["Password"]), Convert.ToString(AccountData["Gender"]), Convert.ToString(AccountData["id"]), balance);
//                AccountList.Add(account);
//            }
//            reader.Close();
//            return AccountList;
//        }
//        public List<Transaction> GetTransactionList()
//        {
//            List<Transaction> TransactionList = new List<Transaction>();
//            string query = "SELECT * FROM [ATM].[dbo].[Transaction]";
//            SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                var transactionData = (IDataReader)reader;
//                string amt = Convert.ToString(transactionData["Amount"]);
//                double amount = double.Parse(amt);
//                string t = Convert.ToString(transactionData["Type"]);
//                int type;
//                if (t == "Credit") { type = 1; }
//                else { type = 2; }
//                Transaction trans = new Transaction(amount, type, Convert.ToString(transactionData["SenderAcountId"]), Convert.ToString(transactionData["RecieverAccountId"]), Convert.ToString(transactionData["SenderBankId"]), Convert.ToString(transactionData["RecieverBankId"]), Convert.ToString(transactionData["id"]));
//                TransactionList.Add(trans);
//            }
//            reader.Close();
//            return TransactionList;
//        }
//        public List<Currency> GetCurrency()
//        {
//            List<Currency> CurrencyList = new List<Currency>();
//            string query = "SELECT * FROM [ATM].[dbo].[Currency]";
//            SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                var CurrencyData = (IDataReader)reader;
//                string er = Convert.ToString(CurrencyData["ExchangeRate"]);
//                double exchangerate = Convert.ToDouble(er);
//                Currency currency = new Currency(Convert.ToString(CurrencyData["CurrencyCode"]), exchangerate);
//                CurrencyList.Add(currency);
//            }
//            reader.Close();
//            return CurrencyList;
//        }

//    }
//}
