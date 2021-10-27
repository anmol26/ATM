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

        public double RTGSChargeToSameBank { get; set; }

        public double IMPSChargeToSameBank { get; set; }

        public double RTGSChargeToOtherBanks { get; set; }

        public double IMPSChargeToOtherBanks { get; set; }

        public CurrencyType AcceptedCurrency { get; set; }

        public double ExchangeRate { get; set; }

        public Bank()
        {
        RTGSChargeToSameBank = 0;         //default rates
        IMPSChargeToSameBank = 0.05;
        RTGSChargeToOtherBanks = 0.02;
        IMPSChargeToOtherBanks = 0.06;
        AcceptedCurrency = CurrencyType.INR;

        }

    }
}
