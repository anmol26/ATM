using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Account
    {
        public static int AccountNumber { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static double Money { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; }

        public static Dictionary<string, string> Users = new Dictionary<string, string>()
        {
                { "Anmol", "1234" },                                              //registered users
                { "Balaji", "0000" }                                              //registered users
        };



    }
}
