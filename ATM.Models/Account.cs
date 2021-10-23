using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CurrentDate { get; set; }

        public static Dictionary<string, string> Users = new Dictionary<string, string>
        {
                { "Anmol", "1234" },                                              //registered users
                { "Balaji", "0000" }                                              //registered users

        };


    }
}
