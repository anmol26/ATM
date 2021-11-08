using System;

namespace ATM.Services
{
    public class GenerateId
    {
        const string DefaultPrefix = "TXN";
        const string DefaultTimeFormat = "ddHHmmss";
        public string GenerateBankId(string bankName)
        {
            string bankId;
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            if (bankName.Length >= 3)
            {
                bankId = bankName.Substring(0, 3).ToUpper() + currentDate;
            }
            else
            {
                bankId = bankName.ToUpper() + currentDate;
            }
            return bankId;
        }
        public string GenerateAccountId(string accName)
        {
            string accId;
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            if (accName.Length >= 3)
            {
                accId = accName.Substring(0, 3).ToUpper() + currentDate;
            }
            else
            {
                accId = accName.ToUpper() + currentDate;
            }
            return accId;
        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            string currentDate = DateTime.Now.ToString(DefaultTimeFormat);
            string txnId = DefaultPrefix + bankId + accountId + currentDate;
            return txnId;
        }
    }
}
