using System;
using ATM.Models;

namespace ATM.CLI
{
    public static class ConsoleOutput
    {
        public static void Welcome()
        {
            Console.WriteLine(Constants.Messages.Welcome);
        }
        public static void Login()
        {
            Console.WriteLine(Constants.Messages.LoginOptions);
        }
        public static void WelcomeUser()
        {
            Console.WriteLine(Constants.Messages.WelcomeUser);
        }
        public static void WrongCredential()
        {
            Console.WriteLine(Constants.Messages.WrongCredential);
        }
        public static void AccountSuccessfullCreation()
        {
            Console.WriteLine(Constants.Messages.AccountSuccessfullCreation);
        }
        public static void BankSuccessfullCreation()
        {
            Console.WriteLine(Constants.Messages.BankSuccessfullCreation);
        }
        public static void CustomerChoice()
        {
            Console.WriteLine(Constants.Messages.CustomerChoice);
        }
        public static void StaffChoice()
        {
            Console.WriteLine(Constants.Messages.StaffChoice);
        }
        public static void TransactionHistory()
        {
            Console.WriteLine(Constants.Messages.TransactionHistory);
        }
        public static void Balance()
        {
            Console.WriteLine(Constants.Messages.Balance);
        }
        public static void InsufficientBalance()
        {
            Console.WriteLine(Constants.Messages.InsufficientBalance);
        }
        public static void InValidOption()
        {
            Console.WriteLine(Constants.Messages.InvalidOption);
        }
        public static void Exit()
        {
            Console.WriteLine(Constants.Messages.Exit);
        }
        public static void BankId(string bankId) 
        {
            Console.WriteLine("\nYour Bank Id is: {0}. PLEASE NOTE IT!!!" , bankId);
        }
        public static void AccountId(string accountId)
        {
            Console.WriteLine("\nYour Account Id is: {0}. PLEASE NOTE IT!!!" , accountId);
        }
        public static void DepositSuccessfull(double amt)
        {
            Console.WriteLine("\n{0} deposited successfully",amt);
        }
        public static void WithdrawSuccessfull(double amt)
        {
            Console.WriteLine("\n{0} withdrawn successfully", amt);
        }
        public static void TransferSuccessfull(double amt)
        {
            Console.WriteLine("\n{0} transferred successfully", amt);
        }
        public static void History(Transaction i)
        {
            Console.WriteLine("Transaction ID:" + i.Id);
            Console.WriteLine(i.Amount);
            Console.WriteLine(i.Type + " to/from your account ");
            if (i.SenderAccountId != i.RecieverAccountId)
            {
                Console.WriteLine("From " + i.SenderAccountId + " to " + i.RecieverAccountId);
            }
            Console.WriteLine(i.CurrentDate.ToString());
        }

    }
}

