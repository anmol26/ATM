using System;
using System.Collections.Generic;

#nullable disable

namespace ATM.Repository.Models
{
    public partial class BankDb
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Branch { get; set; }
        public string Currency { get; set; } = "INR";
        public decimal SameRtgs { get; set; } = 0;
        public decimal SameImps { get; set; } = 5;
        public decimal DiffRtgs { get; set; } = 2;
        public decimal DiffImps { get; set; } = 6;
    }
}
