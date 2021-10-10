using System;
using System.Collections.Generic;
using System.Text;
using ATM.Services;

namespace ATM.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public static string UserName { get; set; }
        public static double Money { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; }

        
       
    }

}
