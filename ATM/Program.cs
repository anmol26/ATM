using System;
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

            OperationType operationOption = (OperationType)Convert.ToInt32(ConsoleInput.Input());
            while (operationOption != OperationType.LogOut)
            {
                if (operationOption == OperationType.Deposit)
                {
                    Console.Clear();
                    double amt = Convert.ToDouble(ConsoleInput.DepositAmount());
                    bankManager.Deposit(amt);
                }
                else if (operationOption == OperationType.Withdraw)
                {
                    Console.Clear();
                    double amt = Convert.ToDouble(ConsoleInput.WithdrawAmount());
                    try
                    {
                        bankManager.Withdraw(amt);
                    }
                    catch (BalanceInsufficientException) 
                    {
                        ConsoleOutput.InsufficientBalance();
                    }
                }
                else if (operationOption == OperationType.Transfer)
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
                else if (operationOption == OperationType.TransactionHistory)
                {
                    Console.Clear();
                    ConsoleOutput.TransactionHistory();
                    bankManager.ShowTransactions();
                    
                }
                else if (operationOption == OperationType.Balance)
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
                operationOption = (OperationType)Convert.ToInt32(ConsoleInput.Input());
            }
            Console.Clear();
            ConsoleOutput.Exit();
        }
        

    }
}

