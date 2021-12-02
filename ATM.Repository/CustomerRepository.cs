using System;
using ATM.Models;
using System.Data.SqlClient;
using System.Data;
using ATM.Repository.Models;
using System.Linq;

namespace ATM.Repository
{
    public class CustomerRepository
    {
        readonly BankDbContext dbContext = new BankDbContext();
        public void UpdateBalance(string id, double balance)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == id)
                    {
                        a.Balance= Convert.ToDecimal(balance);
                        dbContext.Accounts.Update(a);
                        dbContext.SaveChanges();
                    }
                }
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
                var transaction = new TransactionDb
                {
                    Id = transId,
                    SenderAcountId = trans.SenderAccountId,
                    RecieverAccountId=trans.RecieverAccountId,
                    SenderBankId=trans.SenderBankId,
                    RecieverBankId=trans.RecieverBankId,
                    Type=type,
                    Amount=Convert.ToDecimal(amount),
                    CurrentDate=Convert.ToString(DateTime.Now),
                };
                dbContext.Transactions.Add(transaction);
                dbContext.SaveChanges();
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
                foreach (var c in dbContext.Currencies.ToList()) 
                {
                    if (c.CurrencyCode == currCode) 
                    {
                        value = Convert.ToDouble(c.ExchangeRate);
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
