using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Staff : Account
    {
        public Staff(string name, long number, string password, string gender) : base(name, number, password, gender)
        {
            this.Name = name;
            this.PhoneNumber = number;
            this.Password = password;
            this.Gender = gender;
            this.Id = GenerateAccountId(name);
        }
    }
}
