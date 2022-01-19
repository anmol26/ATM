using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATM.API.Models
{
    public class NewBank 
    {

        const string DefaultCurrency = "INR";
        public string BankName { get; set; }
        public string Address { get; set; }
        public string Branch { get; set; }

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
        public string StaffName { get; set; }
        public long SPhoneNumber { get; set; }
        public string SPassword { get; set; }
        public string SGender { get; set; }

    }
}