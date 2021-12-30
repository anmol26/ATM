using System;
using ATM.Models;
using ATM.Repository.Models;
using System.Linq;
using ATM.Models.Enums;

namespace ATM.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ATMContext dbContext;

        //readonly ATMContext dbContext = new ATMContext();
        public CustomerRepository(ATMContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void UpdateBalance(string id, double balance)
        {
            try
            {
                var b = dbContext.Accounts.SingleOrDefault(x => x.Id == id);
                if (b != null)
                {
                    b.Balance = Convert.ToDecimal(balance);
                    dbContext.Accounts.Update(b);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertTransaction(Transaction trans)
        {
            try
            {
                string type = trans.Type == (TransactionType)1?"Credit":"Debit";
                var transaction = new TransactionDb
                {
                    Id = trans.Id,
                    SenderAcountId = trans.SenderAccountId,
                    RecieverAccountId=trans.RecieverAccountId,
                    SenderBankId=trans.SenderBankId,
                    RecieverBankId=trans.RecieverBankId,
                    Type=type,
                    Amount=Convert.ToDecimal(trans.Amount),
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
            try
            {
                var c = dbContext.Currencies.Single(x => x.CurrencyCode == currCode);
                return (Convert.ToDouble(c.ExchangeRate));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
