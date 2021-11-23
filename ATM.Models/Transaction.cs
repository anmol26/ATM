using System;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Transaction
    {
        public Transaction(double amt, int type, string senderId, string recieverId, string sBankId, string rBankId,string id)
        {
            Id = id;
            Type = (TransactionType)type;
            SenderAccountId = senderId;
            RecieverAccountId = recieverId;
            Amount = amt;
            CurrentDate = DateTime.Now;
            SenderBankId = sBankId;
            RecieverBankId = rBankId;
        }
        public string Id { get; set; }
        public string SenderBankId { set; get; }
        public string RecieverBankId { set; get; }
        public string SenderAccountId { set; get; }
        public string RecieverAccountId { set; get; }
        public TransactionType Type { get; set; }
        public DateTime CurrentDate { get; set; }
        public double Amount { get; set; }

    }
}
