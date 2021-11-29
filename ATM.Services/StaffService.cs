using System;
using ATM.Models;
using ATM.Repository;
using System.IO;
using System.Linq;

namespace ATM.Services
{
    public class StaffService
    {
        readonly Library lib = new Library();
        Bank bank;
        static string TransactionListFilename = @"C:\Users\dell\OneDrive\Desktop\TransactionHistory.txt";
        static string StaffListFilename = @"C:\Users\dell\OneDrive\Desktop\StaffList.txt";
        static string CustomerListFilename = @"C:\Users\dell\OneDrive\Desktop\AccountHolderList.txt";
        static string LineSeparater = "\n-----------------------------------------------------------------\n\n";
        readonly CommonServices commonServices = new CommonServices();
        readonly StaffRepository staffOperations = new StaffRepository();
        public string CreateBank(string name, string address, string branch, string currencyCode)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Bank name is not valid!");
                if (lib.GetBankList().Count != 0 & lib.GetBankList().Any(p => p.Name == name))
                    throw new Exception("Bank already exists!");
                if (!lib.GetCurrency().Any(c => c.CurrencyCode == currencyCode))
                    throw new Exception("Invalid currency code!");

                Bank bank = new Bank(name, address, branch, currencyCode, commonServices.GenerateBankId(name));
                staffOperations.InsertNewBank(commonServices.GenerateBankId(name), bank, name, address, branch, currencyCode);
                return bank.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string CreateAccount(string bankId, string name, string password, long phoneNumber, string gender, int choice)
        {
            string Id;
            try
            {
                bank = commonServices.FindBank(bankId);

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Name is not valid!");
                if (lib.GetAccountHolderList().Any(p => p.Name == name) == true)
                    throw new Exception("Account already exists!");
                if (lib.GetBankList().Count != 0 & lib.GetBankList().Any(p => p.Id == bankId) != true)
                    throw new Exception("Bank doesn't exists!");

                if (choice == 1)
                {
                    Staff s = new Staff(bankId, name, phoneNumber, password, gender, commonServices.GenerateAccountId(name));
                    Id = s.Id;
                    staffOperations.InsertNewStaff(s, commonServices.GenerateAccountId(name), name, password, phoneNumber, gender, bankId);
                }
                else
                {
                    Account a = new Account(bankId, name, phoneNumber, password, gender, commonServices.GenerateAccountId(name), 0);
                    Id = a.Id;
                    staffOperations.InsertNewAccount(a, commonServices.GenerateAccountId(name), name, password, phoneNumber, gender, bankId);

                }
                return Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
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

                foreach (var account in lib.GetAccountHolderList().Where(account => account.Name == accountHolder))
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
                user = commonServices.FindAccount(userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            staffOperations.DeleteAccount(user);
        }
        public void AddCurrency(string code, double rate)
        {
            try
            {
                staffOperations.InsertNewCurrency(code, rate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public void UpdateCharges(string bankId, double rtgs, double imps, int choice)
        {
            if (choice == 1)
            {
                bank.SameRTGS = rtgs;
                bank.SameIMPS = imps;
                staffOperations.UpdateCharges(bankId, rtgs, imps, choice);
            }
            else if (choice == 2)
            {
                bank.DiffRTGS = rtgs;
                bank.DiffIMPS = imps;
                staffOperations.UpdateCharges(bankId, rtgs, imps, choice);
            }
        }
        public Account ViewHistory(string Id)
        {
            Account user = null;
            try
            {
                foreach (var account in lib.GetAccountHolderList().Where(account => account.Id == Id))
                {
                    user = account;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

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
                foreach (var i in lib.GetBankList())
                {
                    if (i.Id == bankid)
                    {
                        foreach (var j in lib.GetAccountHolderList())
                        {
                            if (j.Id == accountid)
                            {
                                foreach (var k in lib.GetTransactionList())
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
                foreach (var i in lib.GetBankList())
                {
                    if (i.Id == revert.RecieverBankId)
                    {
                        foreach (var j in lib.GetAccountHolderList())
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
                staffOperations.UpdateBalance(sender.Id, sender.Balance);

                rcvr.Balance -= revert.Amount;
                staffOperations.UpdateBalance(rcvr.Id, rcvr.Balance);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        public void PrintList(Bank bank, int a)
        {
            try
            {
                if (a == 1)
                {
                    string fileName = StaffListFilename;
                    using (StreamWriter file = new StreamWriter(fileName, append: true))
                    {
                        file.WriteLine();
                        foreach (Staff s in lib.GetStaffList())
                        //foreach (Staff s in Library.StaffList)
                        {
                            if (s.BankId == bank.Id)
                            {
                                file.WriteLine(s.Name);
                            }
                        }
                        file.WriteLine(LineSeparater);
                    }
                }
                else
                {
                    string fileName = CustomerListFilename;
                    using (StreamWriter file = new StreamWriter(fileName, append: true))
                    {
                        file.WriteLine();
                        foreach (Account acc in lib.GetAccountHolderList())
                        {
                            if (acc.BankId == bank.Id)
                            {
                                file.WriteLine(acc.Name);
                            }
                        }
                        file.WriteLine(LineSeparater);
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
                string fileName = TransactionListFilename;
                using (StreamWriter file = new StreamWriter(fileName, append: true))
                {
                    file.WriteLine();
                    Bank bank = commonServices.FindBank(bankId);
                    if (bank == null)
                    {
                        throw new Exception("Bank does not exist");
                    }
                    foreach (var account in lib.GetAccountHolderList())
                    {
                        if (account == null)
                        {
                            throw new Exception("Account does not exist");
                        }
                        if (bankId == account.BankId)
                        {
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
                            file.WriteLine(LineSeparater);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateName(Account bankAccount, string name)
        {
            bankAccount.Name = name;
            staffOperations.UpdateName(bankAccount, name);
        }
        public void UpdatePhoneNumber(Account bankAccount, long phoneNumber)
        {
            bankAccount.PhoneNumber = phoneNumber;
            staffOperations.UpdatePhoneNumber(bankAccount, phoneNumber);
        }
        public void UpdatePassword(Account bankAccount, string password)
        {
            bankAccount.Password = password;
            staffOperations.UpdatePassword(bankAccount, password);
        }
    }
}
