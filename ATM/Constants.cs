using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.CLI
{
    public static class Constants
    {
        public static class Messages
        {
            public const string Welcome = "Welcome to Alpha Bank\n<-----*--------*----->\n";
            public const string Login = "Please choose a login Option:- \n1. Setup New Bank \n2. Staff Member \n3. Account Holder";
            public const string LoginOrCreate = "Enter 1, To Create a new account \nEnter 2, To login an existing account\n";
            public const string WelcomeUser = "\n\t!!!!!   You are successfully logged in   !!!!! ";
            public const string WrongCredential = "\nCredentials don't match!, Please Try Again ";
            public const string AlreadyRegistered = " is already taken, Please pick another username";
            public const string SuccessfullCreation = "\n\t!!!!!!   Account Created Successfully   !!!!!!\n";
            public const string Choice = "\n*-----------*-----------*-----------*-----------*\n" +
                "Press 1, \t---\t To deposit money \n" +
                "Press 2, \t---\t To withdraw money \n" +
                "Press 3, \t---\t To transfer money \n" +
                "Press 4, \t---\t To show transaction history \n" +
                "Press 5, \t---\t To see the balance\n " +
                "Press 0, \t---\t To log out \n" +
                "*-----------*-----------*-----------*-----------*\n";
            public const string TransactionHistory = "\nTransaction History:-- \n<--------*-----*------->\n";
            public const string Balance = "\nYour current balance is:";
            public const string InsufficientBalance = "\nInsufficient Balance, Transaction failed !";
            public const string SenderInsufficientBalance = "\nYou do not have enough balance to transfer money";
            public const string ValidOption = "\nPLEASE ENTER A VALID OPTION !";
            public const string Exit = "\n\t\tTHANK YOU, VISIT AGAIN !";
            public const string UserName = "\nPlease Enter the username";
            public const string Password = "\nPlease Enter the Password";
            public const string RecieverName="\nPlease Enter the Reciever name";
            public const string Amount = "\nPlease Enter the Amount";
            public const string BankName = "\nEnter the bank name";




            }
    }
}
