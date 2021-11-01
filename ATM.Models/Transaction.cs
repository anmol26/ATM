using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Transaction
    {
        public Transaction(double amt, int type, string senderId, string recieverId, string sBankId, string rBankId)
        {
            Id = GenerateTransactionId(sBankId,senderId);
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

        public static Dictionary<string,string> Transactions = new Dictionary<string,string>();
        public TransactionType Type { get; set; }
        public DateTime CurrentDate { get; set; }
        public double Amount { get; set; }
        private string GenerateTransactionId(string bankId, string accountId)
        {
            string currentDate = DateTime.Now.ToString("ddHHmmss");
            string txnId = "TXN" + bankId + accountId + currentDate;
            return txnId;
        }



    }
}
