using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class Add
    {
        
        public static void Deposit(double money)
        {
            BankAccount.Money += money;

        }
        
        public static void Deposit()
        {
            Console.WriteLine("Enter amount to deposit in the account");
            string add = Console.ReadLine();
            Add.Deposit(Convert.ToInt32(add));
            Transaction.Transactions.Add($"{add} deposited in account of {BankAccount.UserName} successfully.");
        }
    }
}
