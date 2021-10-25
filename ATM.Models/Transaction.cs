using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }

        public static List<string> Transactions = new List<string>();
        public TransactionType Type { get; set; }
        public DateTime CurrentDate { get; set; }


    }
}
