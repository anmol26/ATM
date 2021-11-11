using System;
using ATM.Models;
using System.IO;
using System.Linq;

namespace ATM.Services
{
    public class CommonServices
    {
        Bank bank;
        const string DefaultPrefix = "TXN";
        const string DefaultTimeFormat = "ddHHmmss";

        public dynamic Login(string bankId, string userId, string pass, string choice)
        {
            try
            {
                bank = FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                if (choice == "1")
                {
                    foreach (var account in bank.StaffAccount.Where(account => account.Id == userId & account.Password == pass))
                    {
                        return account;
                    }
                    
                }
                
                else
                {
                    foreach (var account in bank.UserAccount.Where(account => account.Id == userId & account.Password == pass))
                    {
                        return account;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        public string GenerateBankId(string bankName)
        {
            string bankId;
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            if (bankName.Length >= 3)
            {
                bankId = bankName.Substring(0, 3).ToUpper() + currentDate;
            }
            else
            {
                bankId = bankName.ToUpper() + currentDate;
            }
            return bankId;
        }
        public string GenerateAccountId(string accName)
        {
            string accId;
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            if (accName.Length >= 3)
            {
                accId = accName.Substring(0, 3).ToUpper() + currentDate;
            }
            else
            {
                accId = accName.ToUpper() + currentDate;
            }
            return accId;
        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            string txnId = DefaultPrefix + bankId + accountId + currentDate;
            return txnId;
        }
        public Bank FindBank(string bankId)
        {
            foreach (var i in UserDatabase.Banks.Where(i => i.Id == bankId))
            {
                return i;
            }

            return null;
        }
        public Account FindAccount(Bank bank, string userId)
        {
            foreach (var account in bank.UserAccount.Where(account => account.Id == userId))
            {
                return account;
            }
            return null;
        }
        public void WriteHistory(Transaction i)
        {
            try
            {
                string fileName = @"C:\Users\dell\OneDrive\Desktop\TransactionHistory.txt";
                using (StreamWriter file = new StreamWriter(fileName, append: true))
                {
                    file.WriteLine("Transaction ID:" + i.Id);
                    file.WriteLine(i.Amount);
                    file.WriteLine(i.Type + " to/from your account ");
                    if (i.SenderAccountId != i.RecieverAccountId)
                    {
                        file.WriteLine("From " + i.SenderAccountId + " to " + i.RecieverAccountId);
                    }
                    file.WriteLine(i.CurrentDate.ToString());
                    file.WriteLine("\n-----------------------------------------------------------------\n\n");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
