using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models.Enums;

namespace ATM.Models
{
    public class Staff
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string BankName { get; set; }
        public AccessLevelType AccessLevel { get; set; }
        
    }
}
