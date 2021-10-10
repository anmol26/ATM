using System;
using System.Collections.Generic;
using ATM.Models;
using ATM.Services;


namespace ATM.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Message.Welcome();
            Message.Login();
            
            bool createAccount = (Console.ReadLine() == "1");        // take user input to create account
 
            string userName, password;

            if (createAccount)
            {
                Console.WriteLine("Enter the username:- ");                 // create a new account and add it to users
                userName = Console.ReadLine();
                while (User.Users.ContainsKey(userName))                    // check if userName already exists in users dict if exists ask to pick another userName
                {
                    Console.WriteLine(userName + " is already taken, Please pick another username");
                    userName = Console.ReadLine();
                }
                Console.WriteLine("Enter password");                    // set password 
                password = Console.ReadLine();                          // add user to users dict
                User.Users.Add(userName, password);
                Console.WriteLine("\n!!!!!! Account Created Successfully !!!!!!\n");

            }
            Console.WriteLine("Enter Username");
            userName = Console.ReadLine();
            while (!User.Users.ContainsKey(userName))
            {
                    Console.WriteLine("Enter username");
                    userName = Console.ReadLine();
                    while (!User.Users.ContainsKey(userName))
                    {
                        Console.WriteLine("Username not found, Please try again");
                        userName = Console.ReadLine();
                    }
            }    
            
            Console.WriteLine("\nEnter Password");
            password = Console.ReadLine();
            while (User.Users[userName] != password)
            {
                Console.WriteLine("Wrong password, Please try again");
                password = Console.ReadLine();
            }

            Message.WelcomeUser();
            Message.Choice();
            
            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    Add.Deposit();
                    
                }
                else if (option == "2")
                {
                    Sub.Withdraw();
                }
                else if (option == "3")
                {
                    Transfer.MoneyTransfer();
                }
                else if (option == "4")
                {
                    Transaction.ShowTransactions();
                }
                else if (option == "5")
                {
                    Message.CheckBalance();
                }
                else
                {
                    Message.ValidOption();
                }
                Message.Choice();
                option = Console.ReadLine();
            }

        }

    }
}

