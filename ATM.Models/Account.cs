using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Account
    {
        public Account()
        {
            Id = "ANM31102021";
            Balance = 5000;
            AccountType = AccountType.Savings;
            IsActive = true;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public AccountType AccountType { get; set; }
        public string BankName { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string AccountHolderName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
