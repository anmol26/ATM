using System;
using System.Linq;
using ATM.Models;
using ATM.Repository;
using ATM.Repository.Models;

namespace ATM.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ATMContext dbContext = new ATMContext();
        const string DefaultCurrency = "INR";
        readonly CommonServices commonServices = new CommonServices();
        readonly CustomerRepository customerOperations = new CustomerRepository();
        public void Deposit(Account user, double amount, string currCode)
        {
            try
            {
                user.Balance += Math.Round(amount * (double)(customerOperations.FindExchangeRate(currCode) / customerOperations.FindExchangeRate(DefaultCurrency)), 2);
                Console.WriteLine("Updated Balance is: " + user.Balance);
                customerOperations.UpdateBalance(user.Id, user.Balance);

                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, user.BankId, user.BankId, commonServices.GenerateTransactionId(user.BankId, user.Id));
                customerOperations.InsertTransaction(trans);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Withdraw(Account user, double amount)
        {
            try
            {
                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                    Console.WriteLine("Updated Balance is: " + user.Balance);
                    customerOperations.UpdateBalance(user.Id, user.Balance);

                    Transaction trans = new Transaction(amount, 2, user.Id, user.Id, user.BankId, user.BankId, commonServices.GenerateTransactionId(user.BankId, user.Id));
                    customerOperations.InsertTransaction(trans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool Transfer(Account sender, double amt, Account rcvr, string choice)
        {
            BankDb bank = null;
            BankDb reciever = null;
            try
            {
                foreach (var i in dbContext.Banks.ToList())
                {
                    if (i.Id == sender.BankId)
                    {
                        bank = i;
                    }
                    if (i.Id == rcvr.BankId)
                    {
                        reciever = i;
                    }
                }
                double charge;
                if (sender.BankId == rcvr.BankId)
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

                    Transaction senderTrans = new Transaction(amt+charge, 2, sender.Id, rcvr.Id, sender.BankId, rcvr.BankId, commonServices.GenerateTransactionId(sender.BankId, sender.Id));
                    customerOperations.InsertTransaction(senderTrans);
                    
                    Transaction rcvrTrans = new Transaction(amt, 1, sender.Id, rcvr.Id, sender.BankId, rcvr.BankId, commonServices.GenerateTransactionId(rcvr.BankId, rcvr.Id));
                    customerOperations.InsertTransaction(rcvrTrans);
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
