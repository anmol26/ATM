using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models.Enums
{
    public enum StaffOperationType
    {
        LogOut,
        CreateAccount,
        UpdateAccountStatus,
        UpdateAcceptedCurrency,
        UpdateServiceCharges,
        ShowTransactionHistory,
        RevertTransaction
    }
}
