﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ATM.Repository.Models
{
    public partial class AccountDb
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public string Gender { get; set; }
        public string CurrentDate { get; set; }
        public string BankId { get; set; }
    }
}
