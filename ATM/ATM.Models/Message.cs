using System;
using System.Collections.Generic;
using System.Text;
using ATM.Services;


namespace ATM.Models
{
    public class Message
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Bank Application");
            Console.WriteLine("<-------*---------*------->\n");
        }
        public static void Login()
        {
            Console.WriteLine("Enter 1, To Create a new account");
            Console.WriteLine("Enter any key, To login an existing account");
        }
        public static void WelcomeUser()
        {
            Console.WriteLine("\n");
            Console.WriteLine("!!!!! You are successfully logged in !!!!! ");
        }
        public static void Choice()
        {
            Console.WriteLine("*-----------*-----------*-----------*-----------*\n");
            Console.WriteLine("Press 1, \t---\t To deposit money");
            Console.WriteLine("Press 2, \t---\t To withdraw money");
            Console.WriteLine("Press 3, \t---\t To transfer money");
            Console.WriteLine("Press 4, \t---\t To show transaction history");
            Console.WriteLine("Press 5, \t---\t To see the balance\n");
            Console.WriteLine("Press 0, \t---\t To log out");
        }
        public static void TransactionHistory()
        {
            Console.WriteLine("\nTransaction History:--");
            Console.WriteLine("<--------*-----*------->\n");
        }
        public static void InsufficientBalance()
        {
            Console.WriteLine("\nInsufficient Balance, Transaction failed!!!");
        }
        
    }
}

