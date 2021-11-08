﻿using System.Collections.Generic;

namespace ATM.Models
{
    public class Bank
    {
        const string DefaultCurrency = "INR";
        const double DefaultRTGSChargeToSameBank=0;
        const double DefaultIMPSChargeToSameBank = 5;
        const double DefaultRTGSChargeToOtherBank = 2;
        const double DefaultIMPSChargeToOtherBank = 6;
        public Bank(string name, string address, string branch, string currencyCode, string bankId)
        {
            this.Name = name;
            this.Address = address;
            this.Branch = branch;
            this.Id = bankId;
            this.CurrencyCode = currencyCode;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public List<Account> UserAccount = new List<Account>();
        public List<Staff> StaffAccount = new List<Staff>();

        public double RTGSChargeToSameBank = DefaultRTGSChargeToSameBank;
        public double IMPSChargeToSameBank = DefaultIMPSChargeToSameBank;
        public double RTGSChargeToOtherBanks = DefaultRTGSChargeToOtherBank;
        public double IMPSChargeToOtherBanks = DefaultIMPSChargeToOtherBank;
        public string CurrencyCode = DefaultCurrency;

    }
}
