using System;
using ATM.Models;
using System.Linq;
using ATM.Repository.Models;

namespace ATM.Repository
{
    public class StaffRepository
    {
        readonly BankDbContext dbContext = new BankDbContext(); 
        public void InsertNewBank(Bank bank)
        {
            try
            {
                var newBank = new BankDb
                {
                    Id=bank.Id,
                    Name=bank.Name,
                    Address=bank.Address,
                    Branch=bank.Branch,
                };
                dbContext.Banks.Add(newBank);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertNewStaff(Staff s)
        {
            try
            {
                var newStaff = new StaffDb
                {
                    Id = s.Id,
                    Name=s.Name,
                    Password=s.Password,
                    PhoneNumber=Convert.ToString(s.PhoneNumber),
                    CurrentDate=Convert.ToString(s.CurrentDate),
                    Gender=s.Gender,
                    BankId=s.BankId
                };
                dbContext.Staffs.Add(newStaff);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                int counter = 0;
                foreach (var staff in dbContext.Staffs.Where(staff => staff.BankId == s.BankId).ToList())
                {
                    counter += 1;
                }
                if (counter == 0)
                {
                    DeleteBank(s.BankId);
                }
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewAccount(Account a)
        {
            try
            {
                var newAccount = new AccountDb
                {
                    Id=a.Id,
                    Name=a.Name,
                    Password=a.Password,
                    PhoneNumber= Convert.ToString(a.PhoneNumber),
                    Gender=a.Gender,
                    CurrentDate=Convert.ToString(DateTime.Now),
                    Balance=Convert.ToDecimal(a.Balance),
                    BankId=a.BankId
                };
                dbContext.Accounts.Add(newAccount);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteAccount(Account user)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == user.Id) 
                    {
                        dbContext.Accounts.Remove(a);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewCurrency(string code, double rate)
        {
            try
            {
                var newCurrency = new CurrencyDb
                {
                    CurrencyCode=code,
                    ExchangeRate=Convert.ToDecimal(rate)
                };
                dbContext.Currencies.Add(newCurrency);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCharges(string bankId, double rtgs, double imps, int choice)
        {
            try
            {
                if (choice == 1)
                {
                    foreach (var b in dbContext.Banks.ToList()) 
                    {
                        if (b.Id == bankId) 
                        {
                            b.SameRtgs =Convert.ToDecimal(rtgs);
                            b.SameImps = Convert.ToDecimal(imps);
                            dbContext.Banks.Update(b);
                            dbContext.SaveChanges();
                        }
                    }
                }
                else if (choice == 2)
                {
                    foreach (var b in dbContext.Banks.ToList())
                    {
                        if (b.Id == bankId)
                        {
                            b.DiffRtgs = Convert.ToDecimal(rtgs);
                            b.DiffImps = Convert.ToDecimal(imps);
                            dbContext.Banks.Update(b);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateBalance(string id, double balance)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == id)
                    {
                        a.Balance = Convert.ToDecimal(balance);
                        dbContext.Accounts.Update(a);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateName(Account bankAccount, string name)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == bankAccount.Id)
                    {
                        a.Name = name;
                        dbContext.Accounts.Update(a);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatePhoneNumber(Account bankAccount, long number)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == bankAccount.Id)
                    {
                        a.PhoneNumber=Convert.ToString(number);
                        dbContext.Accounts.Update(a);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatePassword(Account bankAccount, string password)
        {
            try
            {
                foreach (var a in dbContext.Accounts.ToList())
                {
                    if (a.Id == bankAccount.Id)
                    {
                        a.Password = password;
                        dbContext.Accounts.Update(a);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteBank(string bankId)
        {
            try
            {
                foreach (var b in dbContext.Banks.ToList())
                {
                    if (b.Id == bankId)
                    {
                        dbContext.Banks.Remove(b);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
