using System;
using System.Collections.Generic;
using ATM.Models;
using ATM.Services;
using ATM.Models.Enums;


namespace ATM.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleOutput.Welcome();
            ConsoleOutput.Login();

            BankService bankManager = new BankService();


            LoginType loginOption = (LoginType)(Convert.ToInt32(ConsoleInput.Input()));
            var banks = new List<Bank>();    //bank Database
            
            if (loginOption == LoginType.SetupBank)
            {
                string bankName = ConsoleInput.BankName();
                bankManager.CreateBank(bankName);
            }
            if (loginOption == LoginType.StaffMember)
            {   //todo
                StaffMemberLogin();
            }
            if (loginOption == LoginType.AccountHolder)
            {   //todo
                AccountHolderLogin();
            }
            
            ConsoleOutput.LoginOrCreate();

            bool createAccount = (ConsoleInput.Input()== "1");  

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

            ConsoleOutput.WelcomeUser();
            ConsoleOutput.Choice();

            string option = ConsoleInput.Input();
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
                    ConsoleOutput.Balance();
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

            
            static void StaffMemberLogin()
            {   //todo
            }
            static void AccountHolderLogin()
            {   //todo
            }

        }
        

    }
}

