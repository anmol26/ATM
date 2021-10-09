using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class BankAccount
    {
        public static string UserName;
        public static double Money;
       
        public BankAccount(string userName, double money)
        {
            UserName = userName;
            Money = money;
        }
        
       
    }

}
