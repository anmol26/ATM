using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public class User
    {
        const string DefaultTimeFormat = "ddHHmmss";
        public string Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string BankId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CurrentDate { get; set; }
        protected string GenerateAccountId(string accName)
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
    }
}
