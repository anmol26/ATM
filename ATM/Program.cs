using System;
using System.Collections.Generic;
using ATM.Services;
using ATM.Models;


namespace ATM.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Message.Welcome();
            Message.Login();

            bool createAccount = (Console.ReadLine() == "1");        // take user input to create account
 
            string userName;
            string password;

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
            
            Console.WriteLine();
            Console.WriteLine("Enter Password");
            password = Console.ReadLine();
            while (User.Users[userName] != password)
            {
                Console.WriteLine("Wrong password, Please try again");
                password = Console.ReadLine();
            }

            Message.WelcomeUser();
            Message.Choice();

            BankAccount bankAccount1 = new BankAccount(userName, 5000);
            
            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    Console.WriteLine("Enter amount to deposit in the account");
                    string add = Console.ReadLine();
                    bankAccount1.Add(Convert.ToInt32(add));
                    Transaction.Transactions.Add($"{add} deposited in account successfully.");
                }
                else if (option == "2")
                {
                    Console.WriteLine("Enter amount to withdraw from the account");
                    string sub = Console.ReadLine();
                    bankAccount1.Withdraw(Convert.ToInt32(sub));
                    Transaction.Transactions.Add($"{sub} withdrawn from the account." );
                }
                else if (option == "3")
                {  
                    Console.WriteLine("Enter the username to transfer money:- ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter amount to transfer in  account:-");
                    string amount = Console.ReadLine();
                    bankAccount1.Withdraw(Convert.ToInt32(amount));
                    Transaction.Transactions.Add($"{amount} has been transferred to "+username+"'s account successfully.");
                }
                else if (option == "4")
                {
                    Transaction.ShowTransactions();
                }
                else if (option == "5")
                {
                    string a=bankAccount1.Balance();
                    Console.WriteLine(a);
                }
                else
                {
                    Console.WriteLine("Enter a valid option");
                }
                Message.Choice();
                option = Console.ReadLine();
            }

        }

    }
}

