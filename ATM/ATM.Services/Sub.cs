using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class Sub
    {
        public static void Withdraw(double money)
        {
            if (BankAccount.Money < money)
            {
                Message.InsufficientBalance();
            }
            else
            {
                BankAccount.Money -= money;

            }
        }
        public static void Withdraw()
        {
            Console.WriteLine("Enter amount to withdraw from the account");
            string sub = Console.ReadLine();
            Sub.Withdraw(Convert.ToInt32(sub));
            Transaction.Transactions.Add($"{sub} withdrawn from the account of {BankAccount.UserName} successfully.");

        }
    }
}
