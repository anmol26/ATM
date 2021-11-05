
namespace ATM.CLI
{
    public static class Constants
    {
        public static class Messages
        {
            public const string AccountDoesNotExist = "\nAccount does not exist";
            public const string AccountId="\nPlease Enter AccouontId:";
            public const string AccountSuccessfullCreation = "\nAccount Created Successfully!!\n";
            public const string AccountSuccessfullDeletion = "\nAccount deleted successfully!!\n";
            public const string AccountUpdateChoice = "\nWhat do you want to update!\n1.Name\n2.Phone Number\n3.Password\n";
            public const string AdminName = "\nPlease enter Admin name";
            public const string AdminPass = "\nPlease enter Admin password";
            public const string Address = "\nEnter the bank Address";
            public const string Amount = "\nPlease Enter the Amount";
            public const string Balance = "\nYour current Balance is: ";
            public const string BankId = "\nPlease Enter BankId:";
            public const string BankName = "\nEnter the bank name";
            public const string BankSuccessfullCreation = "\nNew Bank Created Successfully!!\n";
            public const string BranchName = "\nPlease enter the Branch name";
            public const string CreateAccountChoice = "\nCreate new account for: \n1.Staff 2.Account Holder";
            public const string CreateFirstStaff = "\nNow Create the First Staff member for this bank:---";
            public const string CurrencyCode = "\nPlease Enter the Currencycode: ";
            public const string DeleteUserName = "\nPlease Enter the username to delete account";
            public const string DepositAmount = "\nPlease Enter the amount to deposit";
            public const string ExchangeRate = "\nEnter Exchange rate:";
            public const string Exit = "\nTHANK YOU, VISIT AGAIN !";
            public const string Gender = "\nPlease enter your Gender";
            public const string InitializeAmount = "\nPlease Enter the Initialize Amount";
            public const string InsufficientBalance = "\nInsufficient Balance, Transaction failed !";
            public const string InvalidDetail = "\nPLEASE ENTER VALID DETAILS !";
            public const string InvalidOption = "\nPLEASE ENTER A VALID OPTION !";
            public const string LoginOptions = "\nPlease choose a login Option:- \n1. Setup New Bank \n2. Staff Member \n3. Account Holder";
            public const string Name = "\nPlease Enter Name:";
            public const string NewCurrencyCode = "\nPlease enter new currencycode to add:";
            public const string NewIMPScharge = "\nEnter new charge for IMPS:";
            public const string NewRTGScharge ="\nEnter new charge for RTGS:";
            public const string Password = "\nPlease Enter the Password";
            public const string PhoneNumber = "\nPlease enter Phone Number";
            public const string ReceiverBankId = "\nEnter Receiver BankId";
            public const string RecieverName="\nPlease Enter the Reciever name";
            public const string RevertAccountId = "\nEnter Accoount Id to revert:";
            public const string RevertBankId = "\nEnter Bank Id to revert:";
            public const string RevertTransactiontId = "\nEnter Transaction Id to revert:";
            public const string SenderBankId = "\nEnter Sender BankId";
            public const string SenderInsufficientBalance = "\nYou do not have enough balance to transfer money";
            public const string ServiceChargeType = "\nSelect type:\n1.RTGS\n2.IMPS";
            public const string ServiceChargeUpdateChoice="Update Service Charges in: \n1.Within Same bank   2.For Different Bank  ";
            public const string StaffName = "\nPlease enter StaffName";
            public const string SuccessfullLogin = "\nLogin Successfull!";
            public const string SetupFirstBank="\nPlease setup a bank first";
            public const string TransactionHistory = "\nTransaction History:-- \n<--------*-----*------->\n";
            public const string TransferToAccountHolderName = "\nEnter Account Holder name to Transfer:";
            public const string UnderConstruction = "\nCurrently application is under construction";
            public const string UpdateCurrency = "\nPlease Enter currency to add";
            public const string UpdateDeleteAccount="\nChoose the Option \n1.Update Account 2.Delete Account";
            public const string UserName = "\nPlease Enter Your Name";
            public const string Welcome = "\nWelcome to Connsole Application \n<----------*-------*---------->\n";
            public const string WelcomeUser = "\nYou are successfully logged in!";
            public const string WithdrawAmount = "\nPlease Enter the amount to withdraw";
            public const string WrongCredential = "\nPlease enter valid credentials";
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
            
        }
    }
}
