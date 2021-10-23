using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;
using ATM.Services;


namespace ATM.CLI


{
    public class Message
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Alpha Bank");
            Console.WriteLine("<-----*--------*----->\n");
        }
        public static void Login()
        {
            Console.WriteLine("Enter 1, To Create a new account");
            Console.WriteLine("Enter 2, To login an existing account\n");
        }
        public static void WelcomeUser()
        {
            Console.WriteLine("\n!!!!! You are successfully logged in !!!!! ");
        }

        public static void AskUserName()
        {
            Console.WriteLine("Enter the username:- ");
        }
        public static void SuccessfullCreation()
        {
            Console.WriteLine("\n!!!!!! Account Created Successfully !!!!!!\n");
        }

        public static void Choice()
        {
            Console.WriteLine("\n*-----------*-----------*-----------*-----------*\n");
            Console.WriteLine("Press 1, \t---\t To deposit money");
            Console.WriteLine("Press 2, \t---\t To withdraw money");
            Console.WriteLine("Press 3, \t---\t To transfer money");
            Console.WriteLine("Press 4, \t---\t To show transaction history");
            Console.WriteLine("Press 5, \t---\t To see the balance\n");
            Console.WriteLine("Press 0, \t---\t To log out");
            Console.WriteLine("*-----------*-----------*-----------*-----------*\n");
        }
        public static void TransactionHistory()
        {
            Console.WriteLine("\n\t\tTransaction History:--");
            Console.WriteLine("\t\t<--------*-----*------->\n");
        }
        public static void InsufficientBalance()
        {
            Console.WriteLine("\n\tInsufficient Balance, Transaction failed!!!");
        }

        public static void ValidOption()
        {
            Console.WriteLine("\n\tPLEASE ENTER A VALID OPTION!!!");
        }
        public static void Exit()
        {
            Console.WriteLine("\n\t\tTHANK YOU, VISIT AGAIN !");
        }
    }
}

