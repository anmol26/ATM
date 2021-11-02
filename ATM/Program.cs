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
        LoginPage:
            ConsoleOutput.Login();
            List<string> loginChoiceOptions = new List<string>(){"1","2","3"}; 
            var chooseOption = ConsoleInput.Input();
            if (loginChoiceOptions.Contains(chooseOption))
            {   goto LoginOptions; }
            else 
            {
                ConsoleOutput.InValidOption();
                goto LoginPage;
            }
        LoginOptions:
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
                Console.WriteLine("\nEnter the Bank Branch name");
                string branch = Console.ReadLine();
                Console.WriteLine("\nEnter the Currencycode: \n1:INR 2:USD 3:EUR");
                string currencyCode = Console.ReadLine();
                string bankId = bankManager.CreateBank(bankName, address, branch, currencyCode);
                ConsoleOutput.BankSuccessfullCreation();
                Console.WriteLine("\nBank Id is: " + bankId);

                Console.WriteLine("\nNow Create the First Staff member for this bank:---");

                Console.WriteLine("\nPlease enter StaffName");
                string sName = Console.ReadLine();
                Console.WriteLine("\nPlease enter Phone Number");
                long sNum = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("\nPlease create Password");
                string sPass = Console.ReadLine();
                Console.WriteLine("\nPlease enter your Gender");
                string sGender = Console.ReadLine();
                string accountId = bankManager.CreateAccount(bankId,sName,sPass,sNum,sGender,1);
                Console.WriteLine("\nYour account Id is: " + accountId);
                ConsoleOutput.WelcomeUser();
                goto LoginPage;
            }
            else if (loginOption == LoginType.StaffMember)
            {   
                Staff bankstaff;
            StaffLogin:
                Console.WriteLine("Enter bankId:");
                string bId = Console.ReadLine();
                Console.WriteLine("Enter AccouontId:");
                string aId = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string pass = Console.ReadLine();

                bankstaff = bankManager.StaffLogin(bId, aId, pass);
                if (bankstaff == null)
                {
                    Console.WriteLine("Invalid details");
                    goto StaffLogin;
                }
                
                ConsoleOutput.WelcomeUser();

            StaffPage:
                ConsoleOutput.StaffChoice();
                List<string> staffChoiceOptions = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7" };
                var chooseStaffOption = ConsoleInput.Input();
                if (staffChoiceOptions.Contains(chooseStaffOption))
                { goto StaffOperations; }
                else
                {
                    ConsoleOutput.InValidOption();
                    goto StaffPage;
                }
            StaffOperations:
                StaffOperationType staffOperation = (StaffOperationType)Convert.ToInt32(chooseStaffOption);
                while (staffOperation != StaffOperationType.LogOut)
                {
                    Account bankAccount;
                    if (staffOperation == StaffOperationType.CreateAccount)
                    {
                        Console.WriteLine("Create new account for: \n1.Staff 2.Account Holder");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nEnter the BankId:- ");
                        string bankId = Console.ReadLine();
                        string name = ConsoleInput.UserName();
                        string password = ConsoleInput.Password();
                        Console.WriteLine("\nEnter Phone Number:");
                        long phoneNumber = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("\nEnter Gender:");
                        string gender = Console.ReadLine();
                        string Id= bankManager.CreateAccount(bankId,name, password, phoneNumber, gender, choice);
                        Console.WriteLine("Your AccountId is: " + Id);
                        ConsoleOutput.AccountSuccessfullCreation();
                    }
                    else if (staffOperation == StaffOperationType.UpdateAccountStatus)
                    {
                        Console.Clear();
                    UpdateAccount:
                        Console.WriteLine("/nChoose the Option /n1.Update Account 2.Delete Account");
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Console.WriteLine("What do you want to update!\n1.Name\n2.Phone Number\n3.Password\n");
                            int uChoice = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter UserId you want to update:");
                            string UserId = Console.ReadLine();
                            Console.WriteLine("Enter Bankid");
                            string bankId = Console.ReadLine();
                            bankAccount = bankManager.UpdateChanges(bankId, UserId);
                            switch (uChoice)
                            {
                                case 1:
                                    Console.WriteLine("Enter Name:");
                                    bankAccount.Name = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.WriteLine("Enter Phone Number:");
                                    bankAccount.PhoneNumber = Convert.ToInt32(Console.ReadLine());
                                    break;
                                case 3:
                                    Console.WriteLine("Enter Password:");
                                    bankAccount.Password = Console.ReadLine();
                                    break;
                                default:
                                    ConsoleOutput.InValidOption();
                                    goto UpdateAccount;
                            }

                        }
                        else if (choice == "2")
                        {
                            Console.WriteLine("Enter UserId you want to delete:");
                            string UserId = Console.ReadLine();
                            Console.WriteLine("Enter Bankid");
                            string bankId = Console.ReadLine();
                            bankManager.DeleteAccount(bankId, UserId);
                            break;
                        }
                        else 
                        {
                            ConsoleOutput.InValidOption();
                            goto UpdateAccount;
                        }
                    }
                    else if (staffOperation == StaffOperationType.UpdateAcceptedCurrency)
                    {
                        Console.WriteLine("Enter currencycode:");
                        string code = Console.ReadLine();
                        Console.WriteLine("Enter Exchange rate:");
                        double rate = Convert.ToDouble(Console.ReadLine());
                        bankManager.AddCurrency(code, rate);
                    }
                    else if (staffOperation == StaffOperationType.UpdateServiceCharges)
                    {
                    UpdateServiceCharge:
                        Console.WriteLine("Update Service Charges in \n1.Within Same bank   2.For Different Bank  ");
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Console.WriteLine("Enter new charge for RTGS:");
                            double Rtgs = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter new charge for IMPS");
                            double Imps = Convert.ToDouble(Console.ReadLine());
                            bankManager.UpdateCharges(Rtgs, Imps, 1);
                        }
                        else if (choice == "2") 
                        {
                            Console.WriteLine("Enter new charge for RTGS:");
                            double Rtgs = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter new charge for IMPS");
                            double Imps = Convert.ToDouble(Console.ReadLine());
                            bankManager.UpdateCharges(Rtgs, Imps, 2);
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
                        Console.WriteLine("Enter AccountId :");
                        string id = Console.ReadLine();
                        bankAccount = bankManager.ViewHistory(id);
                        if (bankAccount == null)
                        {
                            ConsoleOutput.InValidOption();
                            goto ShowTransactionHistory;
                        }
                        foreach (var i in bankAccount.Transactions)
                        {
                            ConsoleOutput.History(i);
                        }
                    }
                    else if (staffOperation == StaffOperationType.RevertTransaction)
                    {
                        Console.WriteLine("Enter Bank Id to revert:");
                        string BankId = Console.ReadLine();
                        Console.WriteLine("Enter User Id to revert:");
                        string UserId = Console.ReadLine();
                        Console.WriteLine("Enter Transaction Id to revert:");
                        string Id = Console.ReadLine();
                        bankManager.RevertTransaction(BankId, UserId, Id);
                    }
                    else if (staffOperation == StaffOperationType.LoginPage)
                    {
                        goto LoginPage;
                    }
                    else
                    {
                        Console.Clear();
                        ConsoleOutput.InValidOption();
                        goto StaffPage;
                    }
                }
                Console.Clear();
                goto Finish;

            }
            else if (loginOption == LoginType.AccountHolder)
            {
                Account bankAccount;
                Console.Clear();
            CustomerLogin:
                Console.WriteLine("Enter bankId:");
                string bId = Console.ReadLine();
                Console.WriteLine("Enter AccouontId:");
                string aId = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                string pass = Console.ReadLine();
                bankAccount = bankManager.Login(bId, aId, pass);
                if (bankAccount == null)
                {
                    Console.WriteLine("Invalid details");
                    goto CustomerLogin;
                }
                else
                {
                    Console.WriteLine("Login Successfull!");
                CustomerPage:
                    ConsoleOutput.CustomerChoice();
                    List<string> customerChoiceOptions = new List<string>() { "0", "1", "2", "3", "4", "5", "6" };
                    var chooseCustomerOption = ConsoleInput.Input();
                    if (customerChoiceOptions.Contains(chooseCustomerOption))
                    { goto CustomerOperations; }
                    else
                    {
                        ConsoleOutput.InValidOption();
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
                            Console.WriteLine("Please Enter Currency Code");
                            string currCode = Console.ReadLine();
                            Console.WriteLine("Enter the BankId");
                            string bankId = Console.ReadLine();
                            bankManager.Deposit(bankAccount, amt, currCode, bankId);
                            Console.WriteLine(amt+" deposited successfuly");
                        }
                        else if (customerOperation == CustomerOperationType.Withdraw)
                        {
                            Console.Clear();
                            double amt = Convert.ToDouble(ConsoleInput.WithdrawAmount());
                            Console.WriteLine("Enter the BankId");
                            string bankId = Console.ReadLine();

                            if (bankManager.Withdraw(bankAccount, amt,bankId))
                            {
                                Console.WriteLine(amt + " withdrawn successfully!");
                            }
                            else
                            {
                                ConsoleOutput.InsufficientBalance();
                            }
                        }
                        else if (customerOperation == CustomerOperationType.Transfer)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Sender BankId");
                            string sbankId = Console.ReadLine();
                            Console.WriteLine("Enter Receiver BankId");
                            string ToBankId = Console.ReadLine();
                            Console.WriteLine("Select type:\n1.RTGS\n2.IMPS");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Account Holder name to Transfer:");
                            string hName = Console.ReadLine();
                            Account reciever = bankManager.CheckAccount(ToBankId, hName);
                            if (reciever != null)
                            {
                                Console.WriteLine("Enter Amount to Transfer:");
                                double amtToTransfer = Convert.ToDouble(Console.ReadLine());
                                if (bankManager.Transfer(bankAccount, amtToTransfer, reciever, sbankId, ToBankId, choice))
                                {
                                    Console.WriteLine(amtToTransfer + " transferred successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient amount to transfer!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Receiver Account does not Exist!");
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
                        goto CustomerPage;
                    }
                }
                 
            }
            else
            {
                Console.Clear();
                ConsoleOutput.InValidOption();
            }
            Console.Clear();
        Finish:
            ConsoleOutput.Exit();
        }
        

    }
}

