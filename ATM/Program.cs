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
            ConsoleOutput.Welcome();
            ConsoleOutput.Login();

            BankService bankManager = new BankService();

            bool createAccount = (Console.ReadLine() == "1");        // take user input to create account

            if (createAccount)
            {
                ConsoleOutput.AskUserId();                 // create a new account and add it to users
                string userId = Console.ReadLine();

                while (Account.Users.ContainsKey(userId))         // check if userName already exists in users dict if exists ask to pick another userName
                {
                    Console.WriteLine(userId + " is already taken, Please pick another username");
                    userId = Console.ReadLine();
                }
                ConsoleOutput.EnterPassword();                           // set password 
                string password = Console.ReadLine();                          // add user to users dict
                Console.WriteLine("Enter the initialize amount");
                double balance = Convert.ToDouble(Console.ReadLine());

                bankManager.CreateAccount(userId, password, balance);

                ConsoleOutput.SuccessfullCreation();

            }
            ConsoleOutput.AskUserId();
            string usrId = Console.ReadLine();
            while (!Account.Users.ContainsKey(usrId))
            {
                ConsoleOutput.AskUserId();
                usrId = Console.ReadLine();
                while (!Account.Users.ContainsKey(usrId))
                {
                    Console.WriteLine("UserId not found, Please try again");
                    usrId = Console.ReadLine();
                }
            }

            ConsoleOutput.EnterPassword();
            string pass = Console.ReadLine();
            while (Account.Users[usrId] != pass)
            {
                Console.WriteLine("Wrong password, Please try again");
                pass = Console.ReadLine();
            }

            ConsoleOutput.WelcomeUser();
            ConsoleOutput.Choice();

            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    ConsoleOutput.EnterAmount();
                    double amt = Convert.ToDouble(Console.ReadLine());
                    bankManager.Deposit(amt);

                }
                else if (option == "2")
                {

                    ConsoleOutput.EnterAmount();
                    double amt = Convert.ToDouble(Console.ReadLine());
                    try
                    {
                        bankManager.Withdraw(amt);
                    }
                    catch (BalanceInsufficientException) 
                    {
                        ConsoleOutput.InsufficientBalance();
                    }
                }
                else if (option == "3")
                {
                    ConsoleOutput.AskUserId();
                    string userName = Console.ReadLine();
                    ConsoleOutput.EnterAmount();
                    double amt = Convert.ToDouble(Console.ReadLine());
                    try
                    {
                        bankManager.Transfer(userName, amt);
                    }
                    catch(SenderBalanceInsufficientException) 
                    {
                        ConsoleOutput.SenderInsufficientBalance();
                    }
                }
                else if (option == "4")
                {
                    ConsoleOutput.TransactionHistory();
                    bankManager.ShowTransactions();
                }
                else if (option == "5")
                {
                    Console.WriteLine(bankManager.Balance());
                }
                else
                {
                    ConsoleOutput.ValidOption();
                }
                ConsoleOutput.Choice();
                option = Console.ReadLine();
            }
            ConsoleOutput.Exit();

        }

    }
}

