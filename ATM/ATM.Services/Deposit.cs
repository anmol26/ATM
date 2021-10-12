using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class Deposit
    {
        
        public static void Add(double money)
        {
            Account.Money += money;
            Transaction.Transactions.Add($"{money} deposited in account of {Account.UserName} successfully.");

        }
        
        public static void Add()
        {
            Console.WriteLine("Enter amount to deposit in the account");
            string add = Console.ReadLine();
            Deposit.Add(Convert.ToDouble(add));
        }
    }
}
