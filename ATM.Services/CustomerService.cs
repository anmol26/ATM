using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;
using System.Linq;

namespace ATM.Services
{
    public class CustomerService
    {
        Bank bank;
        const string DefaultCurrency = "INR";
        readonly CommonServices commonServices = new CommonServices();
        public void Deposit(Account user, double amount, string currCode, string bankId)
        {
            try
            {
                user.Balance += Math.Round(amount * (double)(Currency.curr[currCode] / Currency.curr[DefaultCurrency]), 2);
                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                user.Transactions.Add(trans);
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
                    Transaction trans = new Transaction(amount, 2, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                    user.Transactions.Add(trans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool Transfer(Account user, double amt, Account rcvr, string fromid, string toid, string choice)
        {
            Bank reciever = null;
            try
            {
                foreach (var i in UserDatabase.Banks)
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
                if (user.Balance >= amt + charge)
                {
                    //amt-=charge;
                    //user.Balance -= amt;
                    user.Balance -= amt + charge;

                    rcvr.Balance += Math.Round(amt * (double)(Currency.curr[bank.CurrencyCode] / Currency.curr[reciever.CurrencyCode]), 2);
                    Transaction trans = new Transaction(amt, 2, user.Id, rcvr.Id, fromid, toid, commonServices.GenerateTransactionId(fromid, user.Id));
                    user.Transactions.Add(trans);
                    Transaction rcvrtrans = new Transaction(amt, 1, rcvr.Id, user.Id, fromid, toid, commonServices.GenerateTransactionId(toid, rcvr.Id));
                    rcvr.Transactions.Add(rcvrtrans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Transfer: {0}", ex.Message);
            }
            return false;

        }
        public double DeductCharges(double amount, double percent)
        {
            //return (double)Math.Round(amount * ((100 - Convert.ToDouble(percent)) / 100), 2);
            return (double)Math.Round(amount * (Convert.ToDouble(percent) / 100), 2);
        }
        public double ViewBalance(Account user)
        {
            return user.Balance;
        }
        public Account Login(string bankId, string userId, string pass)
        {
            Account user = null;

            try
            {
                bank = CommonServices.FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                foreach (var account in bank.UserAccount.Where(account => account.Id == userId & account.Password == pass))
                {
                    user = account;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }



    }
}
