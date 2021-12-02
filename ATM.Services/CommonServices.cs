﻿using System;
using ATM.Models;
using ATM.Repository;
using System.IO;
using System.Linq;
using System.Data;

namespace ATM.Services
{
    public class CommonServices
    {
        BankDbContext dbContext = new BankDbContext();
        const string DefaultPrefix = "TXN";
        const string DefaultTimeFormat = "ddHHmmss";
        const string FileName = @"C:\Users\dell\OneDrive\Desktop\TransactionHistory.txt";
        const string LineSeparater = "\n-----------------------------------------------------------------\n\n";
        public dynamic UserLogin(string userId, string pass, string choice)
        {
            try
            {
                if (choice == "1")
                {
                    foreach (var s in dbContext.Staffs.Where(staff => staff.Id == userId & staff.Password == pass).ToList())
                    {
                        var staff = new Staff(s.BankId,s.Name,long.Parse(s.PhoneNumber),s.Password,s.Gender,s.Id);
                        return staff;
                    }
                }

                else
                {
                    foreach (var a in dbContext.Accounts.Where(account => account.Id == userId & account.Password == pass).ToList())
                    {
                        var account = new Account(a.BankId,a.Name,long.Parse(a.PhoneNumber),a.Password,a.Gender,a.Id,Convert.ToDouble(a.Balance));
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
            foreach (var i in dbContext.Banks.Where(i => i.Id == bankId).ToList())
            {
                Bank bank = new Bank(i.Name,i.Address,i.Branch,i.Currency,i.Id);
                return bank;
            }

            return null;
        }
        public Account FindAccount(string userId)
        {
            foreach (var a in dbContext.Accounts.Where(account => account.Id == userId).ToList())
            {
                Account account = new Account(a.BankId,a.Name,long.Parse(a.PhoneNumber),a.Password,a.Gender,a.Id,Convert.ToDouble(a.Balance));
                return account;
            }
            return null;
        }
        public void WriteHistory(Account bankAccount)
        {
            foreach(var t in dbContext.Transactions.Where(trans=> trans.RecieverAccountId== bankAccount.Id || trans.SenderAcountId==bankAccount.Id).ToList())
            try
            {
                using (StreamWriter file = new StreamWriter(FileName, append: true))
                {
                    file.WriteLine("Transaction ID:" + t.Id);
                    file.WriteLine(t.Amount);
                    file.WriteLine(t.Type + " to/from your account ");
                    if (t.SenderAcountId != t.RecieverAccountId)
                    {
                        file.WriteLine("From " + t.SenderAcountId + " to " + t.RecieverAccountId);
                    }
                    file.WriteLine(t.CurrentDate.ToString());
                    file.WriteLine(LineSeparater);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
