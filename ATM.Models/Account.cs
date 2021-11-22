using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public class Account : User
    {
        //const double DefaultBalance = 0;
        public Account(string bankId, string name,long phoneNumber,string password,string gender,string id,double balance)
        {
            this.BankId = bankId;
            this.Name = name;
            this.Id = id;
            this.IsActive = true;
            this.CurrentDate = DateTime.Now;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            this.Password = password;
            Transactions= new List<Transaction>();
            this.Balance = balance;
        }
        //public double balance = DefaultBalance;
        public double Balance { get; set; }
        //{
        //    get
        //    {
        //        return balance;
        //    }
        //    set
        //    {
        //        balance = value;
        //    }
        //}
        public List<Transaction> Transactions { get; set; }
    }
}
