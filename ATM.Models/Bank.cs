using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public class Bank
    {
        const string DefaultCurrency = "INR";
        const double DefaultRTGSChargeToSameBank = 0;
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
        [Key]
        public string Id { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }

        public double sameRTGS = DefaultRTGSChargeToSameBank;
        public double sameIMPS = DefaultIMPSChargeToSameBank;
        public double diffRTGS = DefaultRTGSChargeToOtherBank;
        public double diffIMPS = DefaultIMPSChargeToOtherBank;
        public string currencyCode = DefaultCurrency;

        public string CurrencyCode 
        {
            get
            {
                return currencyCode;
            }
            set
            {
                currencyCode = value;
            }
        }
        public double SameRTGS
        {
            get
            {
                return sameRTGS;
            }
            set
            {
                sameRTGS = value;
            }
        }
        public double DiffRTGS
        {
            get
            {
                return diffRTGS;
            }
            set
            {
                diffRTGS = value;
            }
        }
        public double SameIMPS
        {
            get
            {
                return sameIMPS;
            }
            set
            {
                sameIMPS = value;
            }
        }
        public double DiffIMPS
        {
            get
            {
                return diffIMPS;
            }
            set
            {
                diffIMPS = value;
            }
        }
    }
}
