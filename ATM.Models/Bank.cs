using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Bank
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public List<Account> Accounts { get; set; }

        public double RTGSChargeToSameBank;
        
        public double IMPSChargeToSameBank;
        
        public double RTGSChargeToOtherBanks;
        
        public double IMPSChargeToOtherBanks;
        
        public CurrencyType AcceptedCurrency;
        
        public double ExchangeRate;


    }
}
