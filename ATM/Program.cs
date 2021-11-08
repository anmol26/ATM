using System;
using ATM.Models;
using ATM.Services;
using ATM.Models.Enums;


namespace ATM.CLI
{
    public class Program
    {
        public static void Main(string[] args )
        {
            ConsoleOutput.Welcome();

            BankService bankManager = new BankService();
            Console.WriteLine(Constants.Messages.SetupFirstBank);

        SetupBank:

            string bankName = ConsoleInput.BankName();
            string address = ConsoleInput.Address();
            Console.WriteLine(Constants.Messages.BranchName);
            string branch = Console.ReadLine();
            Console.WriteLine(Constants.Messages.CurrencyCode);
            string currencyCode = Console.ReadLine();
            string bankID;
            try
            {
                bankID = bankManager.CreateBank(bankName, address, branch, currencyCode);
                ConsoleOutput.BankSuccessfullCreation();
                ConsoleOutput.BankId(bankID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto SetupBank;
            }
            Console.WriteLine(Constants.Messages.CreateFirstStaff);

        SetupStaff:
            Console.WriteLine(Constants.Messages.StaffName);
            string sName = Console.ReadLine();
            Console.WriteLine(Constants.Messages.PhoneNumber);
            long sNum;
            try
            {
                sNum = Convert.ToInt64(Console.ReadLine());
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                goto SetupStaff;
            }
            string sPass = ConsoleInput.Password();
            Console.WriteLine(Constants.Messages.Gender);
            string sGender = Console.ReadLine();
            string accountID;
            try
            {
                accountID = bankManager.CreateAccount(bankID, sName, sPass, sNum, sGender, 1);
                ConsoleOutput.AccountId(accountID);
                ConsoleOutput.WelcomeUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto SetupStaff;
            }
        LoginPage:
            ConsoleOutput.Login();
            LoginType loginOption;
            try
            {
                loginOption = (LoginType)(Convert.ToInt32(Console.ReadLine()));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                goto LoginPage;
            }
            Console.Clear();

            if (loginOption == LoginType.SetupBank)
            {
                goto SetupBank;
                
            }
            else if (loginOption == LoginType.StaffMember)
            {   
                Staff bankstaff;
                Console.WriteLine(Constants.Messages.BankId);
                string bId = Console.ReadLine();
                Console.WriteLine(Constants.Messages.AccountId);
                string aId = Console.ReadLine();
                string pass = ConsoleInput.Password();
                try
                {
                    bankstaff = bankManager.StaffLogin(bId, aId, pass);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    goto LoginPage;
                }
                ConsoleOutput.WelcomeUser();

            StaffOperations:
                ConsoleOutput.StaffChoice();
                StaffOperationType staffOperation;
                try
                {
                    staffOperation = (StaffOperationType)Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    goto StaffOperations;
                }
                while (staffOperation != StaffOperationType.LogOut)
                {
                    Account bankAccount;
                    if (staffOperation == StaffOperationType.CreateAccount)
                    {
                        int choice;
                        string bankId, name, password,gender,Id;
                        long phoneNumber;
                        try
                        {
                            Console.WriteLine(Constants.Messages.CreateAccountChoice);
                            choice = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(Constants.Messages.BankId);
                            bankId = Console.ReadLine();
                            name = ConsoleInput.UserName();
                            password = ConsoleInput.Password();
                            Console.WriteLine(Constants.Messages.PhoneNumber);
                            phoneNumber = Convert.ToInt64(Console.ReadLine());
                            Console.WriteLine(Constants.Messages.Gender);
                            gender = Console.ReadLine();
                        }
                        catch (Exception ex)
                        { 
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                        try
                        {
                            Id = bankManager.CreateAccount(bankId, name, password, phoneNumber, gender, choice);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }

                        ConsoleOutput.AccountId(Id);
                        ConsoleOutput.AccountSuccessfullCreation();

                    }
                    else if (staffOperation == StaffOperationType.UpdateAccountStatus)
                    {
                        Console.Clear();
                    UpdateAccount:
                        Console.WriteLine(Constants.Messages.UpdateDeleteAccount);
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            int uChoice;
                            string userId, bankId;
                            try
                            {
                                Console.WriteLine(Constants.Messages.AccountUpdateChoice);
                                uChoice = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(Constants.Messages.AccountId);
                                userId = Console.ReadLine();
                                Console.WriteLine(Constants.Messages.BankId);
                                bankId = Console.ReadLine();
                                bankAccount = bankManager.UpdateChanges(bankId, userId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateAccount;
                            }
                            
                            switch (uChoice)
                            {
                                case 1:
                                    Console.WriteLine(Constants.Messages.Name);
                                    bankAccount.Name = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.WriteLine(Constants.Messages.PhoneNumber);
                                    try
                                    {
                                        bankAccount.PhoneNumber = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception ex) 
                                    {
                                        Console.WriteLine(ex.Message);
                                        goto UpdateAccount;
                                    }
                                    break;
                                case 3:
                                    bankAccount.Password = ConsoleInput.Password();
                                    break;
                                default:
                                    ConsoleOutput.InValidOption();
                                    goto UpdateAccount;
                            }

                        }
                        else if (choice == "2")
                        {
                            string userId, bankId;
                            try
                            {
                                Console.WriteLine(Constants.Messages.AccountId);
                                userId = Console.ReadLine();
                                Console.WriteLine(Constants.Messages.BankId);
                                bankId = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateAccount;
                            }
                            try
                            {
                                bankManager.DeleteAccount(bankId, userId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateAccount;
                            }
                            Console.WriteLine(Constants.Messages.AccountSuccessfullDeletion);
                        }
                        else 
                        {
                            ConsoleOutput.InValidOption();
                            goto UpdateAccount;
                        }
                    }
                    else if (staffOperation == StaffOperationType.UpdateAcceptedCurrency)
                    {
                        string code;
                        double rate;
                        try
                        {
                            Console.WriteLine(Constants.Messages.NewCurrencyCode);
                            code = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.ExchangeRate);
                            rate = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                        bankManager.AddCurrency(code, rate);
                    }
                    else if (staffOperation == StaffOperationType.UpdateServiceCharges)
                    {
                    UpdateServiceCharge:
                        Console.WriteLine(Constants.Messages.ServiceChargeUpdateChoice);
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            double rtgs, imps;
                            try
                            {
                                Console.WriteLine(Constants.Messages.NewRTGScharge);
                                rtgs = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine(Constants.Messages.NewIMPScharge);
                                imps = Convert.ToDouble(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateServiceCharge;
                            }
                            bankManager.UpdateCharges(rtgs, imps, 1);
                        }
                        else if (choice == "2") 
                        {
                            double rtgs, imps;
                            try
                            {
                                Console.WriteLine(Constants.Messages.NewRTGScharge);
                                rtgs = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine(Constants.Messages.NewIMPScharge);
                                imps = Convert.ToDouble(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateServiceCharge;
                            }
                            bankManager.UpdateCharges(rtgs, imps, 2);
                        }
                        else 
                        {
                            ConsoleOutput.InValidOption();
                            goto UpdateServiceCharge;
                        }
                    }
                    else if (staffOperation == StaffOperationType.ShowTransactionHistory)
                    {
                        Console.Clear();
                    ShowTransactionHistory:
                        Console.WriteLine(Constants.Messages.AccountId);
                        string accountId = Console.ReadLine();
                        bankAccount = bankManager.ViewHistory(accountId);
                        if (bankAccount == null)
                        {
                            Console.WriteLine(Constants.Messages.InvalidDetail);
                            goto ShowTransactionHistory;
                        }
                        foreach (var i in bankAccount.Transactions)
                        {
                            ConsoleOutput.History(i);
                        }
                    }
                    else if (staffOperation == StaffOperationType.RevertTransaction)
                    {
                        Console.WriteLine(Constants.Messages.RevertBankId);
                        string bankId = Console.ReadLine();
                        Console.WriteLine(Constants.Messages.RevertAccountId);
                        string accountId = Console.ReadLine();
                        Console.WriteLine(Constants.Messages.RevertTransactiontId);
                        string transId = Console.ReadLine();
                        try
                        {
                            bankManager.RevertTransaction(bankId, accountId, transId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                    }
                    else if (staffOperation == StaffOperationType.LoginPage)
                    {
                        goto LoginPage;
                    }
                    else
                    {
                        Console.Clear();
                        ConsoleOutput.InValidOption();
                    }
                    goto StaffOperations;
                }
                Console.Clear();
                goto Finish;

            }
            else if (loginOption == LoginType.AccountHolder)
            {
                Account bankAccount;
                Console.Clear();

                Console.WriteLine(Constants.Messages.BankId);
                string bId = Console.ReadLine();
                Console.WriteLine(Constants.Messages.AccountId);
                string aId = Console.ReadLine();
                string pass = ConsoleInput.Password();
                try
                {
                    bankAccount = bankManager.Login(bId, aId, pass);
                }
                catch 
                {
                    Console.WriteLine(Constants.Messages.InvalidDetail);
                    goto LoginPage;
                }
                if (bankAccount == null)
                {
                    Console.WriteLine(Constants.Messages.InvalidDetail);
                    goto LoginPage;
                }
                else
                {
                    Console.WriteLine(Constants.Messages.SuccessfullLogin);
                CustomerOperations:
                    ConsoleOutput.CustomerChoice();
                    CustomerOperationType customerOperation;
                    try
                    {
                        customerOperation = (CustomerOperationType)Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto CustomerOperations;
                    }
                    while (customerOperation != CustomerOperationType.LogOut)
                    {

                        if (customerOperation == CustomerOperationType.Deposit)
                        {
                            Console.Clear();
                            double amt;
                            string currCode, bankId;
                            try
                            {
                                amt = Convert.ToDouble(ConsoleInput.DepositAmount());
                                Console.WriteLine(Constants.Messages.CurrencyCode);
                                currCode = Console.ReadLine();
                                Console.WriteLine(Constants.Messages.BankId);
                                bankId = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            try
                            {
                                bankManager.Deposit(bankAccount, amt, currCode, bankId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            ConsoleOutput.DepositSuccessfull(amt);
                        }
                        else if (customerOperation == CustomerOperationType.Withdraw)
                        {
                            Console.Clear();
                            double amt;
                            string bankId;
                            try
                            {
                                amt = Convert.ToDouble(ConsoleInput.WithdrawAmount());
                                Console.WriteLine(Constants.Messages.BankId);
                                bankId = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            if (bankManager.Withdraw(bankAccount, amt,bankId))
                            {
                                ConsoleOutput.WithdrawSuccessfull(amt);
                            }
                            else
                            {
                                ConsoleOutput.InsufficientBalance();
                            }
                        }
                        else if (customerOperation == CustomerOperationType.Transfer)
                        {
                            Console.Clear();
                            Account reciever;
                            Console.WriteLine(Constants.Messages.SenderBankId);
                            string sbankId = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.ReceiverBankId);
                            string ToBankId = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.ServiceChargeType);
                            string choice = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.TransferToAccountHolderName);
                            string hName = Console.ReadLine();
                            try
                            {
                                reciever = bankManager.CheckAccount(ToBankId, hName);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            if (reciever != null)
                            {
                                Console.WriteLine(Constants.Messages.Amount);
                                double amtToTransfer = Convert.ToDouble(Console.ReadLine());
                                if (bankManager.Transfer(bankAccount, amtToTransfer, reciever, sbankId, ToBankId, choice))
                                {
                                    ConsoleOutput.TransferSuccessfull(amtToTransfer);
                                }
                                else
                                {
                                    Console.WriteLine(Constants.Messages.InvalidDetail);
                                }
                            }
                            else
                            {
                                Console.WriteLine(Constants.Messages.AccountDoesNotExist);
                            }

                        }
                        else if (customerOperation == CustomerOperationType.TransactionHistory)
                        {
                            Console.Clear();
                            ConsoleOutput.TransactionHistory();
                            
                            foreach (var i in bankAccount.Transactions)
                            {
                                ConsoleOutput.History(i);
                            }

                        }
                        else if (customerOperation == CustomerOperationType.Balance)
                        {
                            Console.Clear();
                            ConsoleOutput.Balance();
                            Console.Write(bankManager.ViewBalance(bankAccount));
                        }
                        else if (customerOperation == CustomerOperationType.LoginPage)
                        {
                            goto LoginPage;
                        }
                        else
                        {
                            Console.Clear();
                            ConsoleOutput.InValidOption();
                        }
                        goto CustomerOperations;
                    }
                }
                 
            }
            else
            {
                Console.Clear();
                ConsoleOutput.InValidOption();
                goto LoginPage;
            }
            Console.Clear();
        Finish:
            ConsoleOutput.Exit();
        }
        

    }
}

