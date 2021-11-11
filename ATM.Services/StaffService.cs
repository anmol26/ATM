using System;
using ATM.Models;
using System.IO;
using System.Linq;

namespace ATM.Services
{
    public class StaffService
    {
        Bank bank;
        readonly CommonServices commonServices = new CommonServices();
        public string CreateBank(string name, string address, string branch, string currencyCode)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Bank name is not valid!");
            if (UserDatabase.Banks.Count != 0 & UserDatabase.Banks.Any(p => p.Name == name))
                throw new Exception("Bank already exists!");
            if (!Currency.curr.ContainsKey(currencyCode))
                throw new Exception("Invalid currency code!");

            Bank bank = new Bank(name, address, branch, currencyCode, commonServices.GenerateBankId(name));
            UserDatabase.Banks.Add(bank);
            return bank.Id;
        }
        public string CreateAccount(string bankId, string name, string password, long phoneNumber, string gender, int choice)
        {
            string Id;
            bank = commonServices.FindBank(bankId);

            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is not valid!");
            if (bank.UserAccount.Count != 0 & bank.UserAccount.Any(p => p.Name == name) == true)
                throw new Exception("Account already exists!");
            if (UserDatabase.Banks.Count != 0 & UserDatabase.Banks.Any(p => p.Id == bankId) != true)
                throw new Exception("Bank doesn't exists!");

            if (choice == 1)
            {
                Staff s = new Staff(name, phoneNumber, password, gender, commonServices.GenerateAccountId(name));
                bank.StaffAccount.Add(s);
                Id = s.Id;
            }
            else
            {
                Account a = new Account(name, phoneNumber, password, gender, commonServices.GenerateAccountId(name));
                bank.UserAccount.Add(a);
                Id = a.Id;
            }


            return Id;
        }
        public Account CheckAccount(string bankId, string accountHolder)
        {
            Account user = null;
            try
            {
                bank = commonServices.FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }

                foreach (var account in bank.UserAccount.Where(account => account.Name == accountHolder))
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
        public Account UpdateChanges(string bankId, string userId)
        {
            Account user;
            try
            {
                bank = commonServices.FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                user = commonServices.FindAccount(bank, userId);
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
                bank = commonServices.FindBank(bankId);
                if (bank == null)
                {
                    throw new Exception("Bank does not exist");
                }
                user = commonServices.FindAccount(bank, userId);

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
                bank.SameRTGS = rtgs;
                bank.SameIMPS = imps;
            }
            else if (choice == 2)
            {
                bank.DiffRTGS = rtgs;
                bank.DiffIMPS = imps;
            }
        }
        public Account ViewHistory(string Id)
        {
            Account user = null;
            try
            {
                foreach (var account in bank.UserAccount.Where(account => account.Id == Id))
                {
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
                if ((revert == null) || (sender == null))
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
        public void PrintList(Bank bank,int a)
        {
            try
            {
                if (a == 1)
                {
                    string fileName = @"C:\Users\dell\OneDrive\Desktop\StaffList.txt";
                    using (StreamWriter file = new StreamWriter(fileName, append: true))
                    {
                        file.WriteLine();
                        foreach (Staff s in bank.StaffAccount)
                        {
                            file.WriteLine(s.Name);
                        }
                        file.WriteLine("\n-----------------------------------------------------------------\n\n");
                    }
                }
                else 
                {
                    string fileName = @"C:\Users\dell\OneDrive\Desktop\AccountHolderList.txt";
                    using (StreamWriter file = new StreamWriter(fileName, append: true))
                    {
                        file.WriteLine();
                        foreach (Account acc in bank.UserAccount)
                        {
                            file.WriteLine(acc.Name);
                        }
                        file.WriteLine("\n-----------------------------------------------------------------\n\n");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void WriteAllAccountHistory(string bankId)
        {
            try
            {
                string fileName = @"C:\Users\dell\OneDrive\Desktop\TransactionHistory.txt";
                using (StreamWriter file = new StreamWriter(fileName, append: true))
                {
                    file.WriteLine();
                    Bank bank = commonServices.FindBank(bankId);
                    if (bank == null)
                    {
                        throw new Exception("Bank does not exist");
                    }
                    foreach (var account in bank.UserAccount)
                    {
                        if (account == null)
                        {
                            throw new Exception("Account does not exist");
                        }
                        foreach (var transaction in account.Transactions)
                        {
                            file.WriteLine(account.Name);
                            file.WriteLine();
                            file.WriteLine("Transaction ID:" + transaction.Id);
                            file.WriteLine(transaction.Amount);
                            file.WriteLine(transaction.Type + " to/from your account ");
                            if (transaction.SenderAccountId != transaction.RecieverAccountId)
                            {
                                file.WriteLine("From " + transaction.SenderAccountId + " to " + transaction.RecieverAccountId);
                            }
                            file.WriteLine(transaction.CurrentDate.ToString());
                        }
                        file.WriteLine("\n-----------------------------------------------------------------\n\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
