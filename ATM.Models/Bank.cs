using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Bank
    {
        public Bank( )
        {
            Name = "Alpha Bank";
            Id = "ALP31102021";
            Address = "Hyderabad";
            AcceptedCurrency = CurrencyType.INR;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Currency> Currencies { get; set; }
        public double RTGSChargeToSameBank = 0;
        public double IMPSChargeToSameBank = 0.05;
        public double RTGSChargeToOtherBanks = 0.02;
        public double IMPSChargeToOtherBanks = 0.06;
        public CurrencyType AcceptedCurrency { get; set; }
    }
}
