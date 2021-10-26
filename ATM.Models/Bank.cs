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

        public double RTGSChargeToSameBank = 0;         //default rates
        
        public double IMPSChargeToSameBank = 0.05;
        
        public double RTGSChargeToOtherBanks = 0.02;
        
        public double IMPSChargeToOtherBanks = 0.06;
        
        public CurrencyType AcceptedCurrency = CurrencyType.INR;        //default currency type= INR
        
        public double ExchangeRate;


    }
}
