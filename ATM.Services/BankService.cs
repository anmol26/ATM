using System;
using ATM.Models;

namespace ATM.Services
{
    public class BankService
    {
        Bank bank;
        public string CreateBank(string name, string address, string branch, string currencyCode)
        {
            Bank bank = new Bank(name, address, branch, currencyCode);
            UserDatabase.Banks.Add(bank);
            return bank.Id;
        }
        public string CreateAccount(string bankId,string name, string password,long phoneNumber,string gender,int choice)
        {
            string Id="";
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == bankId)
                {
                    bank = i;
                }
            }
            if (choice == 1) 
            {
                Staff s = new Staff(name,phoneNumber,password,gender);
                bank.StaffAccount.Add(s);
                Id = s.Id;
            }
            if (choice == 2)
            {
                Account a = new Account(name, phoneNumber, password, gender);
                bank.UserAccount.Add(a);
                Id = a.Id;
            }
            return Id;
        }
        
        public void Deposit(Account user,double amount, string currCode, string bankId)
        {
            user.Balance += Math.Round(amount * (double)(Currency.curr[currCode] / Currency.curr["INR"]), 2);
            Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId);
            user.Transactions.Add(trans);
        }
        public bool Withdraw(Account user, double amount,string bankId)
        {
            if (user.Balance >= amount)
            {
                user.Balance -= amount;
                Transaction trans = new Transaction(amount, 2, user.Id, user.Id, bankId, bankId);
                user.Transactions.Add(trans);
                return true;
            }
            return false;
        }
        public bool Transfer(Account user, double amt, Account rcvr, string fromid, string toid, int choice)
        {
            Bank reciever = null;
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
                if (choice == 1)
                {
                    charge = DeductCharges(amt, bank.RTGSChargeToSameBank);
                }
                else
                {
                    charge = DeductCharges(amt, bank.IMPSChargeToSameBank);
                }

            }
            else
            {
                if (choice == 1)
                {
                    charge = DeductCharges(amt, bank.RTGSChargeToOtherBanks);
                }
                else
                {
                    charge = DeductCharges(amt, bank.IMPSChargeToOtherBanks);
                }
            }
            if (user.Balance >= amt+charge)
            {
                //amt-=charge;
                //user.Balance -= amt;
                user.Balance -= amt + charge;

                rcvr.Balance += Math.Round(amt * (double)(Currency.curr[bank.CurrencyCode] / Currency.curr[reciever.CurrencyCode]), 2);
                Transaction trans = new Transaction(amt, 2, user.Id, rcvr.Id, fromid, toid);
                user.Transactions.Add(trans);
                Transaction rcvrtrans = new Transaction(amt, 1, rcvr.Id, user.Id, fromid, toid);
                rcvr.Transactions.Add(rcvrtrans);
                return true;
            }
            return false;

        }
        public double DeductCharges(double amount, double percent)
        {
            return (double)Math.Round(amount * ((100 - Convert.ToDouble(percent)) / 100), 2);
        }
        public double ViewBalance(Account user)
        {
            return user.Balance;
        }
        public void ShowTransactions()
        {
            int counter = 1;
            foreach (var transaction in Transaction.Transactions)
            {
                Console.WriteLine($"{counter}-> {transaction.Key}: {transaction.Value}");
                counter += 1;
            }
        }
        public Account Login(string BankId, string UserId, string pass)
        {
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == BankId)
                {
                    bank = i;
                }
            }

            Account user = null;
            foreach (Account account in bank.UserAccount)
            {
                if (account.Id == UserId & account.Password == pass)
                {
                    user = account;
                }
            }
            return user;


        }

        public Staff StaffLogin(string BankId, string UserId, string pass)
        {
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == BankId)
                {
                    bank = i;
                }
            }
            Staff user = null;
            foreach (Staff account in bank.StaffAccount)
            {
                if (account.Id == UserId & account.Password == pass)
                    user = account;

            }
            return user;
        }
        public Account CheckAccount(string BankId, string accountHolder)
        {
            Account user = null;
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == BankId)
                {
                    bank = i;
                }
            }
            foreach (Account account in bank.UserAccount)
            {
                if (account.Name == accountHolder)
                    user = account;
            }
            return user;
        }
        public Account UpdateChanges(string bankId, string userid)
        {
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == bankId)
                {
                    bank = i;
                }
            }
            Account user = null;
            foreach (Account account in bank.UserAccount)
            {
                if (account.Id == userid)
                    user = account;

            }
            return user;

        }
        public void DeleteAccount(string BankId, string UserId)
        {
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == BankId)
                {
                    bank = i;
                }
            }
            Account user = null;
            foreach (var i in bank.UserAccount)
            {
                if (i.Id == UserId)
                {
                    user = i;
                }
            }
            bank.UserAccount.Remove(user);
        }
        public void AddCurrency(string code, double rate)
        {
            Currency.curr[code] = rate;
        }
        public void UpdateCharges(double rtgs, double imps, int choice)
        {
            if (choice == 1)
            {
                bank.RTGSChargeToSameBank = rtgs;
                bank.IMPSChargeToSameBank = imps;
            }
            else if(choice==2)
            {
                bank.RTGSChargeToOtherBanks = rtgs;
                bank.IMPSChargeToOtherBanks = imps;
            }
        }
        public Account ViewHistory(string Id)
        {
            Account user = null;
            foreach (Account account in bank.UserAccount)
            {
                if (account.Id == Id)
                    user = account;

            }
            return user;
        }
        public void RevertTransaction(string bankid, string accountid, string transid)
        {
            Transaction revert = null;
            Account sender = null;
            Account rcvr = null;
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == bankid)
                {
                    foreach (var j in i.UserAccount)
                    {
                        if (j.Id == accountid)
                        {
                            foreach (var k in j.Transactions)
                            {
                                if (k.Id == transid)
                                {
                                    revert = k;
                                    sender = j;

                                }
                            }
                        }
                    }
                }

            }
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == revert.RecieverBankId)
                {
                    foreach (var j in i.UserAccount)
                    {
                        if (j.Id == revert.RecieverAccountId)
                        {
                            rcvr = j;
                        }
                    }
                }
            }
            sender.Balance += revert.Amount;
            rcvr.Balance -= revert.Amount;
        }
    }
}
