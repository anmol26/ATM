using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class BankAccount
    {
        public static string UserName;
        public double Money;


        public BankAccount(string userName, double money)
        {
            UserName = userName;
            Money = money;
        }
        public void Add(double Money)
        {
            this.Money += Money;

        }
        public void Withdraw(double Money)
        {
            if (this.Money < Money)
            {
                Message.InsufficientBalance();
            }
            else
            {
                this.Money -= Money;

            }
        }
        public string Balance()
        {
            return ("\nYour current amount in the Account is: " + this.Money);
        }
        public void Transfer(double money)
        {
            Money -= money;

        }


    }
}
