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
            List<string> mainLoginOptions = new List<string>(){"1","2","3"}; 
            var chooseOption = ConsoleInput.Input();
            if (mainLoginOptions.Contains(chooseOption))
            { 
                goto loginOptions; 
            }
            else 
            {
                ConsoleOutput.ValidOption();
                goto LoginPage;
            }
            loginOptions:
            LoginType loginOption = (LoginType)(Convert.ToInt32(chooseOption));
            
            Console.Clear();
            
            BankService bankManager = new BankService();
            
                if (loginOption == LoginType.SetupBank)
                {
                    string usrId = ConsoleInput.UserName();
                    while (!UserDatabase.AdminUsers.ContainsKey(usrId))
                    {
                        ConsoleOutput.WrongCredential();
                        usrId = ConsoleInput.UserName();
                    }

                    string pass = ConsoleInput.Password();
                    while (UserDatabase.AdminUsers[usrId] != pass)
                    {
                        ConsoleOutput.WrongCredential();
                        pass = ConsoleInput.Password();
                    }
                    Console.Clear();
                    ConsoleOutput.WelcomeUser();

                    string bankName = ConsoleInput.BankName();
                    string address = ConsoleInput.Address();
                    bankManager.CreateBank(bankName, address);

                    ConsoleOutput.BankSuccessfullCreation();
                    goto LoginPage;
                }
                else if (loginOption == LoginType.StaffMember)
                {
                    string staffUserId = ConsoleInput.UserName();
                    while (!UserDatabase.StaffUsers.ContainsKey(staffUserId))
                    {
                        ConsoleOutput.WrongCredential();
                        staffUserId = ConsoleInput.UserName();
                    }

                    string staffPass = ConsoleInput.Password();
                    while (UserDatabase.StaffUsers[staffUserId] != staffPass)
                    {
                        ConsoleOutput.WrongCredential();
                        staffPass = ConsoleInput.Password();
                    }

                    ConsoleOutput.WelcomeUser();

                StaffPage:
                ConsoleOutput.StaffChoice();
                List<string> staffChoiceOptions = new List<string>() {"0","1","2","3","4","5","6","7" };
                var chooseStaffOption = ConsoleInput.Input();
                if (staffChoiceOptions.Contains(chooseStaffOption))
                { goto StaffOperations; }
                else 
                {
                    ConsoleOutput.ValidOption();
                    goto StaffPage;
                }
                StaffOperations:
                StaffOperationType staffOperation = (StaffOperationType)Convert.ToInt32(chooseStaffOption);
                    while (staffOperation != StaffOperationType.LogOut)
                    {
                        if (staffOperation == StaffOperationType.CreateAccount)
                        {
                            Console.Clear();
                            string userId = ConsoleInput.UserName();
                            while (UserDatabase.AccountUsers.ContainsKey(userId))
                            {
                                ConsoleOutput.AlreadyRegistered(userId);
                                userId = ConsoleInput.UserName();
                            }
                            string password = ConsoleInput.Password();
                            bankManager.CreateAccount(userId, password);

                            ConsoleOutput.AccountSuccessfullCreation();
                        }
                        else if (staffOperation == StaffOperationType.UpdateAccountStatus)
                        {
                            Console.Clear();
                            string accountUserName = ConsoleInput.DeleteUserName();
                            if (UserDatabase.AccountUsers.ContainsKey(accountUserName))
                            {
                                UserDatabase.AccountUsers.Remove(accountUserName);
                                ConsoleOutput.AccountSuccessfullDeletion();
                            }
                            else
                            {
                                ConsoleOutput.AccountDoesNotExist();
                            }
                        }
                        else if (staffOperation == StaffOperationType.UpdateAcceptedCurrency)
                        {
                            //todo
                            //how to add in enums
                            ConsoleOutput.UnderConstruction();
                        }
                        else if (staffOperation == StaffOperationType.UpdateServiceCharges)
                        {
                            bankManager.UpdateServiceCharge();
                        }
                        else if (staffOperation == StaffOperationType.ShowTransactionHistory)
                        {
                            Console.Clear();
                            ConsoleOutput.TransactionHistory();
                            bankManager.ShowTransactions();
                        }
                        else if (staffOperation == StaffOperationType.RevertTransaction)
                        {
                            //todo
                            ConsoleOutput.UnderConstruction();
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
                        goto StaffPage;
                    }
                    Console.Clear();
                    goto Finish;

                }
                else if (loginOption == LoginType.AccountHolder)
                {
                    Console.Clear();
                    string usrId = ConsoleInput.UserName();
                    while (!UserDatabase.AccountUsers.ContainsKey(usrId))
                    {
                        ConsoleOutput.WrongCredential();
                        usrId = ConsoleInput.UserName();
                    }

                    string pass = ConsoleInput.Password();
                    while (UserDatabase.AccountUsers[usrId] != pass)
                    {
                        ConsoleOutput.WrongCredential();
                        pass = ConsoleInput.Password();
                    }
                    Console.Clear();
                    ConsoleOutput.WelcomeUser();
                }
                else
                {
                    Console.Clear();
                    ConsoleOutput.ValidOption();
                }
            CustomerPage:
            ConsoleOutput.CustomerChoice();
            List<string> customerChoiceOptions = new List<string>() { "0","1","2","3","4","5","6" };
            var chooseCustomerOption = ConsoleInput.Input();
            if (customerChoiceOptions.Contains(chooseCustomerOption))
            { goto CustomerOperations; }
            else
            {
                ConsoleOutput.ValidOption();
                goto CustomerPage;
            }
            CustomerOperations:

            CustomerOperationType customerOperation = (CustomerOperationType)Convert.ToInt32(chooseCustomerOption);
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
                goto CustomerPage;
            }
            Console.Clear();
            Finish:
            ConsoleOutput.Exit();
        }
        

    }
}

