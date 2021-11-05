using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public class Account : User
    {
        const double DefaultBalance = 0;
        public Account(string name,long phoneNumber,string password,string gender)
        {
            this.Name = name;
            this.Id = GenerateAccountId(name);
            this.IsActive = true;
            this.CurrentDate = DateTime.Now;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            this.Password = password;
        }
        public double Balance = DefaultBalance;
        public List<Transaction> Transactions = new List<Transaction>();

    }
}
