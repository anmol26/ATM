using ATM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Services
{
    public interface ICustomerService
    {
        void Deposit(Account user, double amount, string currCode);
        bool Withdraw(Account user, double amount);
        bool Transfer(Account sender, double amt, Account rcvr, string choice);
        double DeductCharges(double amount, double percent);
        double ViewBalance(Account user);
    }
}
