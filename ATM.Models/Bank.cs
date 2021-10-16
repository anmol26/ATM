using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Bank
    {
        public string Name { get; } = "Alpha Bank";
        public string Address { get; } = "Alpha Bank, Mathura(UP)";
        public List<Account> Accounts { get; set; }
    }
}
