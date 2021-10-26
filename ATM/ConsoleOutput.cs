using System;
using System.Collections.Generic;
using System.Text;

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
        public static void SuccessfullCreation()
        {
            Console.WriteLine(Constants.Messages.SuccessfullCreation);
        }

        public static void Choice()
        {
            Console.WriteLine(Constants.Messages.Choice);
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

        public static void ValidOption()
        {
            Console.WriteLine(Constants.Messages.ValidOption);
        }
        public static void Exit()
        {
            Console.WriteLine(Constants.Messages.Exit);
        }
    }
}

