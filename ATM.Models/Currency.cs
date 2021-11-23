using System.Collections.Generic;


namespace ATM.Models
{
    public class Currency
    {
        public Currency(string currencyCode, double exchangeRate)
        {
            this.CurrencyCode = currencyCode;
            this.Exchangerate = exchangeRate;
        }
        public string CurrencyCode { get; set; }
        public double Exchangerate { get; set; }

    }
    
}
