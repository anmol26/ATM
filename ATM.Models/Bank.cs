using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Bank
    {
        /*public Bank( )
        {
            Name = "Alpha Bank";
            Id = "ALP31102021";
            Address = "Hyderabad";
            AcceptedCurrency = CurrencyType.INR;
        }
        */
        public Bank(string name, string address, string branch, string currencyCode)
        {
            this.Name = name;
            this.Address = address;
            this.Branch = branch;
            this.Id = GenerateBankId(name);
            this.CurrencyCode = currencyCode;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public List<Account> UserAccount = new List<Account>();
        public List<Staff> StaffAccount = new List<Staff>();

        public double RTGSChargeToSameBank = 0;
        public double IMPSChargeToSameBank = 5;
        public double RTGSChargeToOtherBanks = 2;
        public double IMPSChargeToOtherBanks = 6;
        public string CurrencyCode = "INR";

        private string GenerateBankId(string bankName)
        {
            string currentDate = DateTime.Now.ToString("ddHHmmss");
            string bankId = bankName.Substring(0, 3).ToUpper() + currentDate;
            return bankId;
        }

    }
}
