using System;
using System.Collections.Generic;
using System.Text;
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
            Console.WriteLine(Constants.Messages.Login);
        }
        public static void LoginOrCreate() 
        {
            Console.WriteLine(Constants.Messages.LoginOrCreate);
        }
        public static void WelcomeUser()
        {
            Console.WriteLine(Constants.Messages.WelcomeUser);
        }
        public static void WrongCredential()
        {
            Console.WriteLine(Constants.Messages.WrongCredential);
        }
        public static void AlreadyRegistered(string userId) 
        {
            Console.WriteLine(userId + Constants.Messages.AlreadyRegistered);
        }
        public static void AccountSuccessfullCreation()
        {
            Console.WriteLine(Constants.Messages.AccountSuccessfullCreation);
        }
        public static void BankSuccessfullCreation()
        {
            Console.WriteLine(Constants.Messages.BankSuccessfullCreation);
        }
        public static void AccountSuccessfullDeletion()
        {
            Console.WriteLine(Constants.Messages.AccountSuccessfullDeletion);
        }
        public static void AccountDoesNotExist() 
        {
            Console.WriteLine(Constants.Messages.AccountDoesnotExist);
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
        public static void SenderInsufficientBalance() 
        {
            Console.WriteLine(Constants.Messages.SenderInsufficientBalance);
        }

        public static void InValidOption()
        {
            Console.WriteLine(Constants.Messages.InValidOption);
        }
        public static void UnderConstruction()
        {
            Console.WriteLine(Constants.Messages.UnderConstruction);
        }
        public static void Exit()
        {
            Console.WriteLine(Constants.Messages.Exit);
        }
        public static void History(Transaction i)
        {
            Console.WriteLine("Transaction ID:" + i.Id);
            Console.WriteLine(i.Amount);
            Console.WriteLine(i.Type + " to/from your account ");
            if (i.SenderAccountId != i.RecieverAccountId)
            {
                Console.WriteLine("From " + i.SenderAccountId + " to    " + i.RecieverAccountId);
            }
            Console.WriteLine(i.CurrentDate.ToString());
        }

    }
}

