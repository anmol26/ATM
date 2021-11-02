using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public class Account
    {
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
        public string Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }

        public double Balance = 0;
        public string Gender { get; set; }
        public string  BankId { get; set; }

        public List<Transaction> Transactions = new List<Transaction>();
        public bool IsActive { get; set; }
        public DateTime CurrentDate { get; set; }
        protected string GenerateAccountId(string accName)
        {
            string currentDate = DateTime.Now.ToString("ddHHmmss");
            string accId = accName.Substring(0, 3).ToUpper() + currentDate;
            return accId;
        }
    }
}
