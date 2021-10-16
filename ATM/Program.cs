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
 

            if (createAccount)
            {
                Console.WriteLine("Enter the username:- ");                 // create a new account and add it to users
                Account.UserName = Console.ReadLine();
                while (Account.Users.ContainsKey(Account.UserName))         // check if userName already exists in users dict if exists ask to pick another userName
                {
                    Console.WriteLine(Account.UserName + " is already taken, Please pick another username");
                    Account.UserName = Console.ReadLine();
                }
                Console.WriteLine("Enter password");                            // set password 
                Account.Password = Console.ReadLine();                          // add user to users dict
                Account.Users.Add(Account.UserName, Account.Password);
                Console.WriteLine("Enter the initialize amount");
                Account.Money = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("\n!!!!!! Account Created Successfully !!!!!!\n");

            }
            Console.WriteLine("Enter Username");
            Account.UserName = Console.ReadLine();
            while (!Account.Users.ContainsKey(Account.UserName))
            {
                    Console.WriteLine("Enter username");
                    Account.UserName = Console.ReadLine();
                    while (!Account.Users.ContainsKey(Account.UserName))
                    {
                        Console.WriteLine("Username not found, Please try again");
                    Account.UserName = Console.ReadLine();
                    }
            }    
            
            Console.WriteLine("\nEnter Password");
            Account.Password = Console.ReadLine();
            while (Account.Users[Account.UserName] != Account.Password)
            {
                Console.WriteLine("Wrong password, Please try again");
                Account.Password = Console.ReadLine();
            }

            Message.WelcomeUser();
            Message.Choice();
            
            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    Deposit.Add();
                    
                }
                else if (option == "2")
                {
                    Withdraw.Sub();
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
            Message.Exit();

        }

    }
}

