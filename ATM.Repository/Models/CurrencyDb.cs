using System;
using System.Collections.Generic;

#nullable disable

namespace ATM.Repository.Models
{
    public partial class CurrencyDb
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
