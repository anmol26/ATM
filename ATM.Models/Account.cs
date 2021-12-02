using System;

namespace ATM.Models
{
    public class Account : User
    {
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
            this.Balance = balance;
        }
        public double Balance { get; set; }
    }
}
