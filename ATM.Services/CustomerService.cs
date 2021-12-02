using System;
using ATM.Models;
using ATM.Repository;
using ATM.Repository.Models;

namespace ATM.Services
{
    public class CustomerService
    {
        readonly BankDbContext dbContext = new BankDbContext();
        const string DefaultCurrency = "INR";
        readonly CommonServices commonServices = new CommonServices();
        readonly CustomerRepository customerOperations = new CustomerRepository();
        public void Deposit(Account user, double amount, string currCode, string bankId)
        {
            try
            {
                user.Balance += Math.Round(amount * (double)(customerOperations.FindExchangeRate(currCode) / customerOperations.FindExchangeRate(DefaultCurrency)), 2);
                Console.WriteLine("Updated Balance is: " + user.Balance);
                customerOperations.UpdateBalance(user.Id, user.Balance);

                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                string type = "Credit";
                customerOperations.InsertTransaction(commonServices.GenerateTransactionId(bankId, user.Id), type, amount, trans);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Withdraw(Account user, double amount, string bankId)
        {
            try
            {
                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                    Console.WriteLine("Updated Balance is: " + user.Balance);
                    customerOperations.UpdateBalance(user.Id, user.Balance);

                    Transaction trans = new Transaction(amount, 2, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                    string type = "Debit";
                    customerOperations.InsertTransaction(commonServices.GenerateTransactionId(bankId, user.Id), type, amount, trans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool Transfer(Account sender, double amt, Account rcvr, string fromBankId, string toBankId, string choice)
        {
            BankDb bank = null;
            BankDb reciever = null;
            try
            {
                foreach (var i in dbContext.Banks)
                {
                    if (i.Id == fromBankId)
                    {
                        bank = i;
                    }
                    if (i.Id == toBankId)
                    {
                        reciever = i;
                    }
                }
                double charge;
                if (fromBankId == toBankId)
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt,Convert.ToDouble(bank.SameRtgs));
                    }
                    else
                    {
                        charge = DeductCharges(amt, Convert.ToDouble(bank.SameImps));
                    }

                }
                else
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, Convert.ToDouble(bank.DiffRtgs));
                    }
                    else
                    {
                        charge = DeductCharges(amt, Convert.ToDouble(bank.DiffImps));
                    }
                }
                if (sender.Balance >= amt + charge)
                {
                    sender.Balance -= amt + charge;
                    Console.WriteLine("Updated Balance is: " + sender.Balance);
                    customerOperations.UpdateBalance(sender.Id, sender.Balance);

                    rcvr.Balance += Math.Round(amt * (double)(customerOperations.FindExchangeRate(bank.Currency) / customerOperations.FindExchangeRate(reciever.Currency)), 2);
                    customerOperations.UpdateBalance(rcvr.Id, rcvr.Balance);

                    Transaction senderTrans = new Transaction(amt, 2, sender.Id, rcvr.Id, fromBankId, toBankId, commonServices.GenerateTransactionId(fromBankId, sender.Id));
                    string type = "Debit";
                    customerOperations.InsertTransaction(commonServices.GenerateTransactionId(fromBankId, sender.Id), type, amt + charge, senderTrans);
                    
                    Transaction rcvrTrans = new Transaction(amt, 1, sender.Id, rcvr.Id, fromBankId, toBankId, commonServices.GenerateTransactionId(toBankId, rcvr.Id));
                    string type2 = "Credit";
                    customerOperations.InsertTransaction(commonServices.GenerateTransactionId(toBankId, rcvr.Id), type2, amt, rcvrTrans);
                    
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

        }
        public double DeductCharges(double amount, double percent)
        {
            return (double)Math.Round(amount * (Convert.ToDouble(percent) / 100), 2);
        }
        public double ViewBalance(Account user)
        {
            return user.Balance;
        }
    }
}
