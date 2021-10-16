using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class Withdraw
    {
        public static void Sub(double money)
        {
            if (Account.Money < money)
            {
                Console.WriteLine("\n\tInsufficient Balance, Transaction failed!!!");
                //Message.InsufficientBalance();
            }
            else
            {
                Account.Money -= Convert.ToDouble(money);
                Transaction.Transactions.Add($"{money} withdrawn from the account of {Account.UserName} successfully.");
            }
        }
        public static void Sub()
        {
            Console.WriteLine("Enter amount to withdraw from the account");
            string sub = Console.ReadLine();
            Withdraw.Sub(Convert.ToDouble(sub));

        }
    }
}
