using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Bank
    {
        public string Name;
        public string Id;
        public string Address;
        public List<Account> Accounts;

        public double RTGSChargeToSameBank;
        
        public double IMPSChargeToSameBank;
        
        public double RTGSChargeToOtherBanks;
        
        public double IMPSChargeToOtherBanks;
        
        public CurrencyType AcceptedCurrency;
        
        public double ExchangeRate;


    }
}
