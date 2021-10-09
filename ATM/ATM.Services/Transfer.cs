using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class Transfer
    {
        public void MoneyTransfer(double money)
        {
            BankAccount.Money -= money;

        }

        public static void MoneyTransfer()
        {
            Console.WriteLine("Enter the username to transfer money:- ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter amount to transfer in  account:-");
            string amount = Console.ReadLine();
            Sub.Withdraw(Convert.ToInt32(amount));
            Transaction.Transactions.Add($"{amount} has been transferred to " + username + "'s account successfully.");
        }
    }

    
}
