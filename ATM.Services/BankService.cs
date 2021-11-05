using System;
using ATM.Models;
using System.Linq;

namespace ATM.Services
{
    public class BankService
    {
        Bank bank;
        const string DefaultCurrency = "INR";
        public string CreateBank(string name, string address, string branch, string currencyCode)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Bank name is not valid!");
            if (UserDatabase.Banks.Count != 0 & UserDatabase.Banks.Any(p => p.Name == name))
                throw new Exception("Bank already exists!");
            if (!Currency.curr.ContainsKey(currencyCode))
                throw new Exception("Invalid currency code!");
            
            Bank bank = new Bank(name, address, branch, currencyCode);
            UserDatabase.Banks.Add(bank);
            return bank.Id;
        }
        public string CreateAccount(string bankId, string name, string password, long phoneNumber, string gender, int choice)
        {
            string Id;
            bank = FindBank(bankId);

            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is not valid!");
            if (bank.UserAccount.Count != 0 & bank.UserAccount.Any(p => p.Name == name) == true)
                throw new Exception("Account already exists!");
            if (UserDatabase.Banks.Count != 0 & UserDatabase.Banks.Any(p => p.Id == bankId) != true)
                throw new Exception("Bank doesn't exists!");

            if (choice == 1)
                {
                    Staff s = new Staff(name, phoneNumber, password, gender);
                    bank.StaffAccount.Add(s);
                    Id = s.Id;
                }
            else
                {
                    Account a = new Account(name, phoneNumber, password, gender);
                    bank.UserAccount.Add(a);
                    Id = a.Id;
                }
            
            
            return Id;
        }
        
        public void Deposit(Account user,double amount, string currCode, string bankId)
        {
            try
            {
                user.Balance += Math.Round(amount * (double)(Currency.curr[currCode] / Currency.curr[DefaultCurrency]), 2);
                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId);
                user.Transactions.Add(trans);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Withdraw(Account user, double amount,string bankId)
        {
            try
            {
                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                    Transaction trans = new Transaction(amount, 2, user.Id, user.Id, bankId, bankId);
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
                        charge = DeductCharges(amt, bank.RTGSChargeToSameBank);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.IMPSChargeToSameBank);
                    }

                }
                else
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, bank.RTGSChargeToOtherBanks);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.IMPSChargeToOtherBanks);
                    }
                }
                if (user.Balance >= amt + charge)
                {
                    //amt-=charge;
                    //user.Balance -= amt;
                    user.Balance -= amt + charge;

                    //rcvr.Balance += Math.Round(amt * (double)(Currency.curr[bank.CurrencyCode] / Currency.curr[reciever.CurrencyCode]), 2);
                    rcvr.Balance += Math.Round(amt * (double)( Currency.curr[reciever.CurrencyCode]/ Currency.curr[bank.CurrencyCode]), 2);
                    Transaction trans = new Transaction(amt, 2, user.Id, rcvr.Id, fromid, toid);
                    user.Transactions.Add(trans);
                    Transaction rcvrtrans = new Transaction(amt, 1, rcvr.Id, user.Id, fromid, toid);
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
        public void ShowTransactions()
        {
            try
            {
                int counter = 1;
                foreach (var transaction in Transaction.Transactions)
                {
                    Console.WriteLine($"{counter}-> {transaction.Key}: {transaction.Value}");
                    counter += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Show Transactions: {0}", ex.Message);
            }
        }
        public Account Login(string bankId, string userId, string pass)
        {
            Account user = null;
            
            try
            {
                bank = FindBank(bankId);
                foreach (Account account in bank.UserAccount)
                {
                    if (account.Id == userId & account.Password == pass)
                    {
                        user = account;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public Staff StaffLogin(string bankId, string userId, string pass)
        {
            Staff user=null;
            try
            {
                bank = FindBank(bankId);
                if (bank == null) 
                {
                    throw new Exception("Bank does not exist");
                }
                foreach (Staff account in bank.StaffAccount)
                {
                    if (account.Id == userId & account.Password == pass)
                        user = account;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public Account CheckAccount(string bankId, string accountHolder)
        {
            Account user = null;
            try
            {
                bank = FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                foreach (Account account in bank.UserAccount)
                {
                    if (account.Name == accountHolder)
                        user = account;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public Account UpdateChanges(string bankId, string userId)
        {
            Account user;
            try
            {
                bank = FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                user = FindAccount(bank, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;

        }
        public void DeleteAccount(string bankId, string userId)
        {
            Account user;
            try
            {
                bank = FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                user = FindAccount(bank, userId);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

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
            try
            {
                foreach (Account account in bank.UserAccount)
                {
                    if (account.Id == Id)
                        user = account;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ViewHistory: {0}", ex.Message);

            }
            return user;
        }
        public void RevertTransaction(string bankid, string accountid, string transid)
        {
            Transaction revert = null;
            Account sender = null;
            Account rcvr = null;
            try
            {
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
                if((revert==null) || (sender== null))
                {
                    throw new Exception();
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                sender.Balance += revert.Amount;
                rcvr.Balance -= revert.Amount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        public static Bank FindBank(string bankId)
        {
            foreach (var i in UserDatabase.Banks)
            {
                if (i.Id == bankId)
                {
                    return i;
                }
            }
            return null;
        }
        public static Account FindAccount(Bank bank, string userId)
        {
            foreach (Account account in bank.UserAccount)
            {
                if (account.Id == userId )
                    return account;

            }
            return null;
        }
    }
}
