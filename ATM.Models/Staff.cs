using System;

namespace ATM.Models
{
    public class Staff : User
    {
        public Staff(string bankId, string name, long number, string password, string gender,string id)
        {
            this.BankId = bankId;
            this.Name = name;
            this.PhoneNumber = number;
            this.Password = password;
            this.IsActive = true;
            this.CurrentDate = DateTime.Now;
            this.Gender = gender;
            this.Id = id;
        }
    }
}
