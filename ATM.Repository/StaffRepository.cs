using System;
using ATM.Models;
using System.Linq;
using ATM.Repository.Models;

namespace ATM.Repository
{
    public class StaffRepository
    {
        readonly ATMDbContext dbContext = new ATMDbContext();
        public void InsertNewBank(Bank bank)
        {
            try
            {
                var newBank = new BankDb
                {
                    Id = bank.Id,
                    Name = bank.Name,
                    Address = bank.Address,
                    Branch = bank.Branch,
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
                    Name = s.Name,
                    Password = s.Password,
                    PhoneNumber = Convert.ToString(s.PhoneNumber),
                    CurrentDate = Convert.ToString(s.CurrentDate),
                    Gender = s.Gender,
                    BankId = s.BankId
                };
                dbContext.Staffs.Add(newStaff);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                //rollback
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewAccount(Account a)
        {
            try
            {
                var newAccount = new AccountDb
                {
                    Id = a.Id,
                    Name = a.Name,
                    Password = a.Password,
                    PhoneNumber = Convert.ToString(a.PhoneNumber),
                    Gender = a.Gender,
                    CurrentDate = Convert.ToString(DateTime.Now),
                    Balance = Convert.ToDecimal(a.Balance),
                    BankId = a.BankId
                };
                dbContext.Accounts.Add(newAccount);
                dbContext.SaveChanges();
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
                var a = dbContext.Accounts.SingleOrDefault(x => x.Id == userId);
                if (a == null)
                {
                    throw new Exception("Account does not exist");
                }
                else 
                {
                    dbContext.Accounts.Remove(a);
                    dbContext.SaveChanges();
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
                    CurrencyCode = code,
                    ExchangeRate = Convert.ToDecimal(rate)
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
                    var b = dbContext.Banks.SingleOrDefault(x=> x.Id==bankId);
                    if (b == null)
                    {
                        throw new Exception();
                    }
                    else 
                    {

                        b.SameRtgs = Convert.ToDecimal(rtgs);
                        b.SameImps = Convert.ToDecimal(imps);
                        dbContext.Banks.Update(b);
                        dbContext.SaveChanges();
                    }
                }
                else if (choice == 2)
                {
                    var b = dbContext.Banks.SingleOrDefault(x => x.Id == bankId);
                    if (b == null)
                    {
                        throw new Exception();
                    }
                    else
                    {

                        b.DiffRtgs = Convert.ToDecimal(rtgs);
                        b.DiffImps = Convert.ToDecimal(imps);
                        dbContext.Banks.Update(b);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateBalance(string accountId, double balance)
        {
            try
            {
                var a = dbContext.Accounts.SingleOrDefault(x=>x.Id==accountId);
                if (a == null)
                {
                    throw new Exception();
                }
                else 
                {
                    a.Balance = Convert.ToDecimal(balance);
                    dbContext.Accounts.Update(a);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateAccount(string accountId, string? name, long? phoneNumber, string? password) 
        {
            try
            {
                var a = dbContext.Accounts.SingleOrDefault(y => y.Id == accountId);
                if (a == null)
                {
                    throw new Exception();
                }
                else
                {
                    a.Name = name == null ? a.Name : name;
                    a.PhoneNumber = phoneNumber == null ? a.PhoneNumber : Convert.ToString(phoneNumber);
                    a.Password = password == null ? a.Password : password;
                    dbContext.Accounts.Update(a);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
