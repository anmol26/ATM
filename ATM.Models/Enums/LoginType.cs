using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models.Enums
{
    public enum LoginType
    {
        SetupBank=1,
        StaffMember,
        AccountHolder,
        CreateAccount,
        ExistingAccount
    }
}
