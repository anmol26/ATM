using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;
using ATM.Services;


namespace ATM.CLI


{
    public class ConsoleOutput
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Alpha Bank");
            Console.WriteLine("<-----*--------*----->\n");
        }
        public static void Login()
        {
            Console.WriteLine("Please choose a login Option");
            Console.WriteLine("1. Setup New Bank");
            Console.WriteLine("2. Staff Member");
            Console.WriteLine("3. Account Holder");
        }
        public static void LoginOrCreate() 
        {
            Console.WriteLine("Enter 1, To Create a new account");
            Console.WriteLine("Enter 2, To login an existing account\n");
        }
        public static void WelcomeUser()
        {
            Console.WriteLine("\n\t!!!!!   You are successfully logged in   !!!!! ");
        }
        public static void WrongCredential()
        {
            Console.WriteLine("\nCredentials don't match!, Please Try Again ");
        }
        public static void AlreadyRegistered(string userId) 
        {
            Console.WriteLine(userId + " is already taken, Please pick another username");
        }
        public static void SuccessfullCreation()
        {
            Console.WriteLine("\n\t!!!!!!   Account Created Successfully   !!!!!!\n");
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
            Console.WriteLine("\nTransaction History:--");
            Console.WriteLine("<--------*-----*------->\n");
        }
        public static void Balance()
        {
            Console.WriteLine("\nYour current balance is:");
        }
        public static void InsufficientBalance()
        {
            Console.WriteLine("\nInsufficient Balance, Transaction failed !");
        }
        public static void SenderInsufficientBalance() 
        {
            Console.WriteLine("\nYou do not have enough balance to transfer money");
        }

        public static void ValidOption()
        {
            Console.WriteLine("\nPLEASE ENTER A VALID OPTION !");
        }
        public static void Exit()
        {
            Console.WriteLine("\n\t\tTHANK YOU, VISIT AGAIN !");
        }
    }
}

