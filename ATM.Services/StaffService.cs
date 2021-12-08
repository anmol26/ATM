using System;
using ATM.Models;
using ATM.Repository;
using ATM.Repository.Models;
using System.IO;
using System.Linq;

namespace ATM.Services
{
    public class StaffService
    {
        readonly ATMContext dbContext= new ATMContext();
        Bank bank;
        static string TransactionListFilename = @"C:\Users\dell\OneDrive\Desktop\TransactionHistory.txt";
        static string StaffListFilename = @"C:\Users\dell\OneDrive\Desktop\StaffList.txt";
        static string CustomerListFilename = @"C:\Users\dell\OneDrive\Desktop\AccountHolderList.txt";
        static string LineSeparater = "\n-----------------------------------------------------------------\n\n";
        readonly CommonServices commonServices = new CommonServices();
        readonly StaffRepository staffOperations = new StaffRepository();
        public string CreateBank(string name, string address, string branch, string currencyCode, string sName, string sPass, long sPhone, string gender)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Bank name is not valid!");
                if (dbContext.Banks.Any(p => p.Name == name))
                    throw new Exception("Bank already exists!");
                if (!dbContext.Currencies.Any(c => c.CurrencyCode == currencyCode))
                    throw new Exception("Invalid currency code!");

                Bank bank = new Bank(name, address, branch, currencyCode, commonServices.GenerateBankId(name));
                Staff s = new Staff(bank.Id, sName, sPhone, sPass, gender, commonServices.GenerateAccountId(sName));
                staffOperations.InsertNewBank(bank,s);
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
                if (dbContext.Accounts.Any(p => p.Name == name) == true)
                    throw new Exception("Account already exists!");
                if (dbContext.Banks.Any(p => p.Id == bankId) != true)
                    throw new Exception("Bank doesn't exists!");

                if (choice == 1)
                {
                    Staff s = new Staff(bankId, name, phoneNumber, password, gender, commonServices.GenerateAccountId(name));
                    Id = s.Id;
                    staffOperations.InsertNewStaff(s);
                }
                else
                {
                    Account a = new Account(bankId, name, phoneNumber, password, gender, commonServices.GenerateAccountId(name), 0);
                    Id = a.Id;
                    staffOperations.InsertNewAccount(a);

                }
                return Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public void DeleteAccount(string userId)
        {
            try
            {
                staffOperations.DeleteAccount(userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            bank = commonServices.FindBank(bankId);
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
        public void RevertTransaction(string transid)
        {
            TransactionDb revertTransaction;
            AccountDb sender;
            AccountDb rcvr;
            try
            {
                revertTransaction = dbContext.Transactions.Single(x=> x.Id==transid);
                sender = dbContext.Accounts.Single(x => x.Id == revertTransaction.SenderAcountId);
                rcvr = dbContext.Accounts.Single(x=> x.Id== revertTransaction.RecieverAccountId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                sender.Balance += revertTransaction.Amount;
                staffOperations.UpdateBalance(sender.Id,Convert.ToDouble(sender.Balance));
                rcvr.Balance -= revertTransaction.Amount;
                staffOperations.UpdateBalance(rcvr.Id,Convert.ToDouble(rcvr.Balance));
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
                        foreach (var s in dbContext.Staffs.ToList())
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
                        foreach (var acc in dbContext.Accounts.ToList())
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
                    foreach (var account in dbContext.Accounts.ToList())
                    {
                        if (account == null)
                        {
                            throw new Exception("Account does not exist");
                        }
                        if (bankId == account.BankId)
                        {
                            foreach (var transaction in dbContext.Transactions)
                            {
                                file.WriteLine(account.Name);
                                file.WriteLine();
                                file.WriteLine("Transaction ID:" + transaction.Id);
                                file.WriteLine(transaction.Amount);
                                file.WriteLine(transaction.Type + " to/from your account ");
                                if (transaction.SenderAcountId != transaction.RecieverAccountId)
                                {
                                    file.WriteLine("From " + transaction.SenderAcountId + " to " + transaction.RecieverAccountId);
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
            staffOperations.UpdateAccount(bankAccount.Id,name,null,null);
        }
        public void UpdatePhoneNumber(Account bankAccount, long phoneNumber)
        {
            bankAccount.PhoneNumber = phoneNumber;
            staffOperations.UpdateAccount(bankAccount.Id,null,phoneNumber,null);
        }
        public void UpdatePassword(Account bankAccount, string password)
        {
            bankAccount.Password = password;
            staffOperations.UpdateAccount(bankAccount.Id, null, null, password);
        }
    }
}
