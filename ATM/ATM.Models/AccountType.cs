using System;
using System.Collections.Generic;
using System.Text;
using ATM.Services;

namespace ATM.Models
{
    public enum AccountType
    {
        Savings = 1,
        Current ,
        RecurringDeposit ,
        FixedDeposit ,
        Demat ,
        NRI
    }
}
