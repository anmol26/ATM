using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public class Account : User
    {
        const double DefaultBalance = 0;
        public Account(string name,long phoneNumber,string password,string gender,string id)
        {
            this.Name = name;
            this.Id = id;
            this.IsActive = true;
            this.CurrentDate = DateTime.Now;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            this.Password = password;
            Transactions= new List<Transaction>();
        }
        public double Balance = DefaultBalance;
        public List<Transaction> Transactions { get; set; }
    }
}
