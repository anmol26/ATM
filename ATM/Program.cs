﻿using System;
using System.Collections.Generic;
using ATM.Models;
using ATM.Services;
using ATM.Models.Enums;
using ATM.Models.Exceptions;


namespace ATM.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ConsoleOutput.Welcome1);
            ConsoleOutput.Welcome();
            ConsoleOutput.Login();
            LoginType loginOption = (LoginType)(Convert.ToInt32(ConsoleInput.Input()));
            Console.Clear();

            BankService bankManager = new BankService();

            if (loginOption == LoginType.SetupBank)
            {
                string bankName = ConsoleInput.BankName();
                bankManager.CreateBank(bankName);
            }
            if (loginOption == LoginType.StaffMember)
            {   
                string staffUserId = ConsoleInput.UserName();
                while (!Staff.Users.ContainsKey(staffUserId))
                {
                    ConsoleOutput.WrongCredential();
                    staffUserId = ConsoleInput.UserName();
                }

                string staffPass = ConsoleInput.Password();
                while (Staff.Users[staffUserId] != staffPass)
                {
                    ConsoleOutput.WrongCredential();
                    staffPass = ConsoleInput.Password();
                }

                ConsoleOutput.WelcomeUser();
            }
            if (loginOption == LoginType.AccountHolder)
            {
                Console.Clear();
                ConsoleOutput.LoginOrCreate();

                bool createAccount = (ConsoleInput.Input() == "1");

                if (createAccount)
                {
                    string userId = ConsoleInput.UserName();

                    while (Account.Users.ContainsKey(userId))
                    {
                        ConsoleOutput.AlreadyRegistered(userId);
                        userId = ConsoleInput.UserName();
                    }
                    string password = ConsoleInput.Password();
                    double balance = Convert.ToDouble(ConsoleInput.Amount());

                    bankManager.CreateAccount(userId, password, balance);

                    ConsoleOutput.SuccessfullCreation();

                }
                string usrId = ConsoleInput.UserName();
                while (!Account.Users.ContainsKey(usrId))
                {
                    ConsoleOutput.WrongCredential();
                    usrId = ConsoleInput.UserName();
                }

                string pass = ConsoleInput.Password();
                while (Account.Users[usrId] != pass)
                {
                    ConsoleOutput.WrongCredential();
                    pass = ConsoleInput.Password();
                }
                Console.Clear();
                ConsoleOutput.WelcomeUser();
            }

            ConsoleOutput.Choice();

            string option = ConsoleInput.Input();
            while (option != "0")
            {
                if (option == "1")
                {
                    Console.Clear();
                    double amt = Convert.ToDouble(ConsoleInput.Amount());
                    bankManager.Deposit(amt);
                }
                else if (option == "2")
                {
                    Console.Clear();
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
                    Console.Clear();
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
                    Console.Clear();
                    ConsoleOutput.TransactionHistory();
                    bankManager.ShowTransactions();
                    
                }
                else if (option == "5")
                {
                    Console.Clear();
                    ConsoleOutput.Balance();
                    Console.WriteLine(bankManager.Balance());
                }
                else
                {
                    Console.Clear();
                    ConsoleOutput.ValidOption();
                }
                ConsoleOutput.Choice();
                option = Console.ReadLine();
            }
            Console.Clear();
            ConsoleOutput.Exit();
        }
        

    }
}

