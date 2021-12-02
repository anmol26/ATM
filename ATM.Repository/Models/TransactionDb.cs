using System;
using System.Collections.Generic;

#nullable disable

namespace ATM.Repository.Models
{
    public partial class TransactionDb
    {
        public string Id { get; set; }
        public string SenderAcountId { get; set; }
        public string RecieverAccountId { get; set; }
        public string SenderBankId { get; set; }
        public string RecieverBankId { get; set; }
        public string Type { get; set; }
        public string CurrentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
