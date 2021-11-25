using System;
using ATM.Models;
using ATM.Repository;

namespace ATM.Services
{
    public class CustomerService
    {
        const string DefaultCurrency = "INR";
        readonly CommonServices commonServices = new CommonServices();
        readonly CustomerRepository customerOperations = new CustomerRepository();
        public void Deposit(Account user, double amount, string currCode, string bankId)
        {
            try
            {

                user.Balance += Math.Round(amount * (double)(customerOperations.FindExchangeRate(currCode) / customerOperations.FindExchangeRate(DefaultCurrency)), 2);
                Console.WriteLine("Updated Balance is: "+user.Balance);
                customerOperations.UpdateBalance(user.Id, user.Balance);

                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                user.Transactions.Add(trans);
                string type = "Credit";
                customerOperations.InsertTransaction(bankId,user, commonServices.GenerateTransactionId(bankId, user.Id),type,amount,trans);
                
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
                    user.Transactions.Add(trans);
                    string type = "Debit";
                    customerOperations.InsertTransaction(bankId, user, commonServices.GenerateTransactionId(bankId, user.Id), type, amount, trans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool Transfer(Account sender, double amt, Account rcvr, string fromid, string toid, string choice)
        {
            Bank bank=null;
            Bank reciever = null;
            try
            {
                foreach (var i in Library.BankList)
                {
                    if (i.Id == fromid)
                    {
                        bank = i;
                    }
                    if (i.Id == toid)
                    {
                        reciever = i;
                    }
                }
                double charge;
                if (fromid == toid)
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, bank.SameRTGS);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.SameIMPS);
                    }

                }
                else
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, bank.DiffRTGS);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.DiffIMPS);
                    }
                }
                if (sender.Balance >= amt + charge)
                {
                    sender.Balance -= amt + charge;
                    Console.WriteLine("Updated Balance is: " + sender.Balance);
                    customerOperations.UpdateBalance(sender.Id, sender.Balance);


                    rcvr.Balance += Math.Round(amt * (double)(customerOperations.FindExchangeRate(bank.CurrencyCode) / customerOperations.FindExchangeRate(reciever.CurrencyCode)), 2);
                    customerOperations.UpdateBalance(rcvr.Id, rcvr.Balance);

                    Transaction senderTrans = new Transaction(amt, 2, sender.Id, rcvr.Id, fromid, toid, commonServices.GenerateTransactionId(fromid, sender.Id));
                    sender.Transactions.Add(senderTrans);
                    string type = "Debit";
                    customerOperations.InsertTransaction(fromid, sender, commonServices.GenerateTransactionId(fromid, sender.Id), type, amt+charge, senderTrans);
                    Library.TransactionList.Add(senderTrans);

                    Transaction rcvrTrans = new Transaction(amt, 1, sender.Id, rcvr.Id, fromid, toid, commonServices.GenerateTransactionId(toid, rcvr.Id));
                    rcvr.Transactions.Add(rcvrTrans);
                    string type2 = "Credit";
                    customerOperations.InsertTransaction(toid, rcvr, commonServices.GenerateTransactionId(toid, rcvr.Id), type2, amt, rcvrTrans);
                    Library.TransactionList.Add(rcvrTrans);

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
