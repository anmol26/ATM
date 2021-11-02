
namespace ATM.CLI
{
    public static class Constants
    {
        public static class Messages
        {
            public const string Welcome = "Welcome to Alpha Bank\n<-----*--------*----->\n";
            public const string Login = "\nPlease choose a login Option:- \n1. Setup New Bank \n2. Staff Member \n3. Account Holder";
            public const string LoginOrCreate = "\nEnter 1, To Create a new account \nEnter 2, To login an existing account\n";
            public const string WelcomeUser = "\nYou are successfully logged in!";
            public const string WrongCredential = "\nCredentials don't match!, Please Try Again ";
            public const string AlreadyRegistered = " is already taken, Please pick another username";
            public const string AccountSuccessfullCreation = "\nAccount Created Successfully!!\n";
            public const string AccountSuccessfullDeletion = "\nAccount deleted successfully!!\n";
            public const string AccountDoesnotExist = "\nAccount does not exist!!\n";
            public const string BankSuccessfullCreation = "\nNew Bank Created Successfully!!\n";
            public const string CustomerChoice = "\n*-----------*-----------*-----------*-----------*\n" +
                "Press 1, \t---\t To deposit money \n" +
                "Press 2, \t---\t To withdraw money \n" +
                "Press 3, \t---\t To transfer money \n" +
                "Press 4, \t---\t To show transaction history \n" +
                "Press 5, \t---\t To see the balance\n" +
                "Press 6, \t---\t To Login Another Account\n" +
                "Press 0, \t---\t To log out \n" +
                "*-----------*-----------*-----------*-----------*\n";
            public const string StaffChoice = "\n*-----------*-----------*-----------*-----------*\n" +
                "Press 1, \t---\t To Create new Account \n" +
                "Press 2, \t---\t To Update/Delete Account \n" +
                "Press 3, \t---\t To Update Accepted Currency\n" +
                "Press 4, \t---\t To Update Service Charges\n" +
                "Press 5, \t---\t To show transaction history \n" +
                "Press 6, \t---\t To Revert Transaction\n" +
                "Press 7, \t---\t To Login Another Account\n" +
                "Press 0, \t---\t To log out \n" +
                "*-----------*-----------*-----------*-----------*\n";
            public const string TransactionHistory = "\nTransaction History:-- \n<--------*-----*------->\n";
            public const string Balance = "\nYour current balance is: ";
            public const string InsufficientBalance = "\nInsufficient Balance, Transaction failed !";
            public const string SenderInsufficientBalance = "\nYou do not have enough balance to transfer money";
            public const string InValidOption = "\nPLEASE ENTER A VALID OPTION !";
            public const string Exit = "\nTHANK YOU, VISIT AGAIN !";
            public const string UserName = "\nPlease Enter the username";
            public const string Password = "\nPlease Enter the Password";
            public const string RecieverName="\nPlease Enter the Reciever name";
            public const string DepositAmount = "\nPlease Enter the amount to deposit";
            public const string WithdrawAmount = "\nPlease Enter the amount to withdraw";
            public const string Amount = "\nPlease Enter the Amount";
            public const string InitializeAmount = "\nPlease Enter the Initialize Amount";
            public const string BankName = "\nEnter the bank name";
            public const string Address = "\nEnter the bank Address";
            public const string UnderConstruction = "\nCurrently application is under construction";
            public const string DeleteUserName = "\nPlease Enter the username to delete account";
            public const string UpdateCurrency = "\nPlease Enter currency to add";

        }
    }
}
