using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM.API.Models
{
    public class NewAccount
    {

        public string BankId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public long PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}