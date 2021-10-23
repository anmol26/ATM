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

            BankManager bankManager = new BankManager();

            bool createAccount = (Console.ReadLine() == "1");        // take user input to create account

            if (createAccount)
            {
                Console.WriteLine("Enter the UserId:- ");                 // create a new account and add it to users
                string userId = Console.ReadLine();

                while (Account.Users.ContainsKey(userId))         // check if userName already exists in users dict if exists ask to pick another userName
                {
                    Console.WriteLine(userId + " is already taken, Please pick another username");
                    userId = Console.ReadLine();
                }
                Console.WriteLine("Enter password");                            // set password 
                string password = Console.ReadLine();                          // add user to users dict
                Console.WriteLine("Enter the initialize amount");
                double balance = Convert.ToDouble(Console.ReadLine());

                bankManager.CreateAccount(userId, password, balance);

                Console.WriteLine("\n!!!!!! Account Created Successfully !!!!!!\n");

            }
            Console.WriteLine("Enter UserId");
            string usrId = Console.ReadLine();
            while (!Account.Users.ContainsKey(usrId))
            {
                Console.WriteLine("Enter UserId");
                usrId = Console.ReadLine();
                while (!Account.Users.ContainsKey(usrId))
                {
                    Console.WriteLine("UserId not found, Please try again");
                    usrId = Console.ReadLine();
                }
            }

            Console.WriteLine("\nEnter Password");
            string pass = Console.ReadLine();
            while (Account.Users[usrId] != pass)
            {
                Console.WriteLine("Wrong password, Please try again");
                pass = Console.ReadLine();
            }

            Message.WelcomeUser();
            Message.Choice();

            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    Console.WriteLine("Enter amount to deposit in the account");
                    double amt = Convert.ToDouble(Console.ReadLine());
                    bankManager.Deposit(amt);

                }
                else if (option == "2")
                {

                    Console.WriteLine("Enter amount to withdraw from the account");
                    double amt = Convert.ToDouble(Console.ReadLine());
                    bankManager.Withdraw(amt);
                }
                else if (option == "3")
                {
                    Console.WriteLine("Enter the username to transfer money:- ");
                    string userName = Console.ReadLine();
                    Console.WriteLine("Enter amount to transfer in account:-");
                    double amt = Convert.ToDouble(Console.ReadLine());
                    bankManager.Transfer(userName, amt);
                }
                else if (option == "4")
                {
                    Message.TransactionHistory();
                    bankManager.ShowTransactions();
                }
                else if (option == "5")
                {
                    Console.WriteLine(bankManager.Balance());
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

