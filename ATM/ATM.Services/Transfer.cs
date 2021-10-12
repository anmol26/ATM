﻿using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;
using ATM.App;

namespace ATM.Services
{
    public class Transfer
    {
        public static void MoneyTransfer()
        {
            Console.WriteLine("Enter the username to transfer money:- ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter amount to transfer in  account:-");
            string amount = Console.ReadLine();
            if (Convert.ToDouble(amount) < Account.Money)
            {
                Withdraw.Sub(Convert.ToInt32(amount));
                Transaction.Transactions.Add($"{amount} has been transferred to " + username + "'s account successfully.");
            }
            else 
            {
                Message.InsufficientBalance();
                //throw new Exception("Exception");
                
            }
        }
    }

    
}
