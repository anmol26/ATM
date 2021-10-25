using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Bank
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }

        public readonly DateTime currentDate;
        

    }
}
