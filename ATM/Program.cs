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
                string userId = ConsoleInput.UserName();

                while (Account.Users.ContainsKey(userId))         // check if userName already exists in users dict if exists ask to pick another userName
                {
                    ConsoleOutput.AlreadyRegistered(userId);
                    userId = ConsoleInput.UserName();
                }
                ConsoleOutput.EnterPassword();                           // set password 
                string password = Console.ReadLine();                          // add user to users dict
                double balance = Convert.ToDouble(ConsoleInput.Amount());

                bankManager.CreateAccount(userId, password, balance);

                ConsoleOutput.SuccessfullCreation();

            }
            string usrId = ConsoleInput.UserName();
            while (!Account.Users.ContainsKey(usrId))
            {
                usrId = ConsoleInput.UserName();
                while (!Account.Users.ContainsKey(usrId))
                {
                    ConsoleOutput.WrongCredential();
                    usrId = ConsoleInput.UserName();
                }
            }

            ConsoleOutput.EnterPassword();
            string pass = Console.ReadLine();
            while (Account.Users[usrId] != pass)
            {

                ConsoleOutput.WrongCredential();
                pass = Console.ReadLine();
            }

            ConsoleOutput.WelcomeUser();
            ConsoleOutput.Choice();

            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    double amt = Convert.ToDouble(ConsoleInput.Amount());
                    bankManager.Deposit(amt);

                }
                else if (option == "2")
                {
                    double amt = Convert.ToDouble(ConsoleInput.Amount());
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
                    string userName = ConsoleInput.RecieverName();
                    double amt = Convert.ToDouble(ConsoleInput.Amount());
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

