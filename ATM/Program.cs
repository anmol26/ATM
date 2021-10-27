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
            LoginPage:
            ConsoleOutput.Login();

            LoginType loginOption = (LoginType)(Convert.ToInt32(ConsoleInput.Input()));
            Console.Clear();
            
            BankService bankManager = new BankService();

            if (loginOption == LoginType.SetupBank)
            {
                string bankName = ConsoleInput.BankName();
                string address = ConsoleInput.Address();
                bankManager.CreateBank(bankName,address);

                ConsoleOutput.BankSuccessfullCreation();
                goto Finish;
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
                ConsoleOutput.StaffChoice();

                StaffOperationType staffOperation = (StaffOperationType)Convert.ToInt32(ConsoleInput.Input());
                while (staffOperation != StaffOperationType.LogOut)
                {
                    if (staffOperation == StaffOperationType.CreateAccount)
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

                        ConsoleOutput.AccountSuccessfullCreation();
                    }
                    else if (staffOperation == StaffOperationType.UpdateAccountStatus)
                    {
                        //todo
                    }
                    else if (staffOperation == StaffOperationType.UpdateAcceptedCurrency)
                    {
                        //todo
                    }
                    else if (staffOperation == StaffOperationType.UpdateServiceCharges)
                    {
                        //todo
                    }
                    else if (staffOperation == StaffOperationType.ShowTransactionHistory)
                    {
                        //todo
                    }
                    else if (staffOperation == StaffOperationType.RevertTransaction)
                    {
                        //todo
                    }
                    else if (staffOperation == StaffOperationType.LoginPage) 
                    {
                        goto LoginPage;
                    }
                    else
                    {
                        Console.Clear();
                        ConsoleOutput.ValidOption();
                    }
                    ConsoleOutput.StaffChoice();
                    staffOperation = (StaffOperationType)Convert.ToInt32(ConsoleInput.Input());
                }
                Console.Clear();
                ConsoleOutput.UnderConstruction();
                goto Finish;

            }
            if (loginOption == LoginType.AccountHolder)
            {
                Console.Clear();
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

            ConsoleOutput.CustomerChoice();

            CustomerOperationType customerOperation = (CustomerOperationType)Convert.ToInt32(ConsoleInput.Input());
            while (customerOperation != CustomerOperationType.LogOut)
            {
                if (customerOperation == CustomerOperationType.Deposit)
                {
                    Console.Clear();
                    double amt = Convert.ToDouble(ConsoleInput.DepositAmount());
                    bankManager.Deposit(amt);
                }
                else if (customerOperation == CustomerOperationType.Withdraw)
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
                else if (customerOperation == CustomerOperationType.Transfer)
                {
                    Console.Clear();
                    string userName = ConsoleInput.RecieverName();
                    double amt = Convert.ToDouble(ConsoleInput.Amount());
                    try
                    {
                        bankManager.Transfer(userName, amt);
                    }
                    catch (SenderBalanceInsufficientException)
                    {
                        ConsoleOutput.SenderInsufficientBalance();
                    }

                }
                else if (customerOperation == CustomerOperationType.TransactionHistory)
                {
                    Console.Clear();
                    ConsoleOutput.TransactionHistory();
                    bankManager.ShowTransactions();

                }
                else if (customerOperation == CustomerOperationType.Balance)
                {
                    Console.Clear();
                    ConsoleOutput.Balance();
                    Console.WriteLine(bankManager.Balance());
                }
                else if (customerOperation == CustomerOperationType.LoginPage) 
                {
                    goto LoginPage;
                }
                else
                {
                    Console.Clear();
                    ConsoleOutput.ValidOption();
                }
                ConsoleOutput.CustomerChoice();
                customerOperation = (CustomerOperationType)Convert.ToInt32(ConsoleInput.Input());
            }
            Console.Clear();
            Finish:
            ConsoleOutput.Exit();
        }
        

    }
}

