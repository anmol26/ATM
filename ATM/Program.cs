using ATM.Models;
using ATM.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using ATM.Services;
using System;

namespace ATM.CLI
{
    public class Program
    {

        public static readonly IServiceProvider container = DIContainerBuilder.Build();
        public static void Main(string[] args)
        {
            IStaffService staffMember = container.GetService<IStaffService>();
            ICustomerService accountHolder = container.GetService<ICustomerService>();
            ICommonService commonServices = container.GetService<ICommonService>();
            ConsoleOutput.Welcome();

        //StaffService staffMember = new StaffService();
        //CustomerService aaccountHolder = new CustomerService();
        //CommonServices commonServices = new CommonServices();
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
            SetupBank:
                string bankName = ConsoleInput.BankName();
                string address = ConsoleInput.Address();
                Console.WriteLine(Constants.Messages.BranchName);
                string branch = Console.ReadLine();
                Console.WriteLine(Constants.Messages.CurrencyCode);
                string currencyCode = Console.ReadLine();
                string bankID;

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
                try
                {
                    bankID = staffMember.CreateBank(bankName, address, branch, currencyCode, sName, sPass, sNum, sGender);
                    ConsoleOutput.BankSuccessfullCreation();
                    ConsoleOutput.BankId(bankID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto SetupBank;
                }
                goto LoginPage;

            }
            else if (loginOption == LoginType.StaffMember)
            {
                Staff bankstaff;
                Console.WriteLine(Constants.Messages.AccountId);
                string aId = Console.ReadLine();
                string pass = ConsoleInput.Password();
                try
                {
                    bankstaff = commonServices.UserLogin(aId, pass, "1");
                    if (bankstaff == null)
                    {
                        throw new Exception(Constants.Messages.AccountDoesNotExist);
                    }
                }
                catch (Exception ex)
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
                        string name, password, gender, Id;
                        long phoneNumber;
                        try
                        {
                            Console.WriteLine(Constants.Messages.CreateAccountChoice);
                            choice = Convert.ToInt32(Console.ReadLine());
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
                            Id = staffMember.CreateAccount(bankstaff.BankId, name, password, phoneNumber, gender, choice);
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
                            string userId;
                            try
                            {
                                Console.WriteLine(Constants.Messages.AccountUpdateChoice);
                                uChoice = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(Constants.Messages.AccountId);
                                userId = Console.ReadLine();
                                bankAccount = commonServices.FindAccount(userId);
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
                                    string name = Console.ReadLine();
                                    staffMember.UpdateName(bankAccount, name);
                                    break;
                                case 2:
                                    Console.WriteLine(Constants.Messages.PhoneNumber);
                                    try
                                    {
                                        long phoneNumber = Convert.ToInt64(Console.ReadLine());
                                        staffMember.UpdatePhoneNumber(bankAccount, phoneNumber);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        goto UpdateAccount;
                                    }
                                    break;
                                case 3:
                                    string password = ConsoleInput.Password();
                                    staffMember.UpdatePassword(bankAccount, password);
                                    break;
                                default:
                                    ConsoleOutput.InValidOption();
                                    goto UpdateAccount;
                            }

                        }
                        else if (choice == "2")
                        {
                            string userId;
                            try
                            {
                                Console.WriteLine(Constants.Messages.AccountId);
                                userId = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto UpdateAccount;
                            }
                            try
                            {
                                staffMember.DeleteAccount(userId);
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
                        staffMember.AddCurrency(code, rate);
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
                            staffMember.UpdateCharges(bankstaff.BankId, rtgs, imps, 1);
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
                            staffMember.UpdateCharges(bankstaff.BankId, rtgs, imps, 2);
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
                        try
                        {
                            bankAccount = commonServices.FindAccount(accountId);
                            Console.WriteLine(Constants.Messages.StaffTransactionHistoryChoice);
                            string choice = Console.ReadLine();
                            if (choice == "1")
                            {
                                if (bankAccount == null)
                                {
                                    Console.WriteLine(Constants.Messages.InvalidDetail);
                                    goto ShowTransactionHistory;
                                }
                                ConsoleOutput.TransactionHistory();
                                commonServices.TransactionHistory(bankAccount);

                            }
                            else if (choice == "2")
                            {
                                if (bankAccount == null)
                                {
                                    Console.WriteLine(Constants.Messages.InvalidDetail);
                                    goto ShowTransactionHistory;
                                }
                                commonServices.WriteHistory(bankAccount);
                            }
                            else
                            {
                                Console.Clear();
                                ConsoleOutput.InValidOption();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                    }
                    else if (staffOperation == StaffOperationType.RevertTransaction)
                    {
                        Console.WriteLine(Constants.Messages.RevertTransactiontId);
                        string transId = Console.ReadLine();
                        try
                        {
                            staffMember.RevertTransaction(transId);
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
                    else if (staffOperation == StaffOperationType.ShowStaffMemberList)
                    {
                        Bank bank;
                        Console.WriteLine(Constants.Messages.BankId);
                        string bankId = Console.ReadLine();
                        bank = commonServices.FindBank(bankId);
                        if (bank == null)
                        {
                            Console.WriteLine(Constants.Messages.BankNotExist);
                        }
                        else
                        {
                            staffMember.PrintList(bank, 1);
                            Console.WriteLine(Constants.Messages.StaffListSuccessFull);
                        }
                    }
                    else if (staffOperation == StaffOperationType.ShowCustomerMemberList)
                    {
                        Bank bank;
                        Console.WriteLine(Constants.Messages.BankId);
                        string bankId = Console.ReadLine();
                        bank = commonServices.FindBank(bankId);
                        if (bank == null)
                        {
                            Console.WriteLine(Constants.Messages.BankNotExist);
                        }
                        else
                        {
                            staffMember.PrintList(bank, 2);
                            Console.WriteLine(Constants.Messages.AccountHolderListSuccessFull);
                        }
                    }
                    else if (staffOperation == StaffOperationType.TransferMoney)
                    {
                        Console.Clear();
                        Account senderAccount, recieverAccount;
                        Console.WriteLine(Constants.Messages.SenderAccountId);
                        string senderAccountId = Console.ReadLine();
                        Console.WriteLine(Constants.Messages.ReceiverAccountId);
                        string recieverAccountId = Console.ReadLine();
                        Console.WriteLine(Constants.Messages.ServiceChargeType);
                        string choice = Console.ReadLine();
                        Console.WriteLine();
                        try
                        {
                            senderAccount = commonServices.FindAccount(senderAccountId);
                            recieverAccount = commonServices.FindAccount(recieverAccountId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                        if (recieverAccount != null)
                        {
                            Console.WriteLine(Constants.Messages.Amount);
                            double amtToTransfer = Convert.ToDouble(Console.ReadLine());
                            if (accountHolder.Transfer(senderAccount, amtToTransfer, recieverAccount, choice))
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
                    else if (staffOperation == StaffOperationType.DepositMoney)
                    {
                        Console.Clear();
                        double amt;
                        string currCode, accountID;
                        try
                        {
                            amt = Convert.ToDouble(ConsoleInput.DepositAmount());
                            Console.WriteLine(Constants.Messages.CurrencyCode);
                            currCode = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.AccountId);
                            accountID = Console.ReadLine();
                            bankAccount = commonServices.FindAccount(accountID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                        try
                        {
                            accountHolder.Deposit(bankAccount, amt, currCode);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto StaffOperations;
                        }
                        ConsoleOutput.DepositSuccessfull(amt);

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

                Console.WriteLine(Constants.Messages.AccountId);
                string aId = Console.ReadLine();
                string pass = ConsoleInput.Password();
                try
                {
                    bankAccount = commonServices.UserLogin(aId, pass, "2");
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
                            string currCode;
                            try
                            {
                                amt = Convert.ToDouble(ConsoleInput.DepositAmount());
                                Console.WriteLine(Constants.Messages.CurrencyCode);
                                currCode = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            try
                            {
                                accountHolder.Deposit(bankAccount, amt, currCode);
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
                            try
                            {
                                amt = Convert.ToDouble(ConsoleInput.WithdrawAmount());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            if (accountHolder.Withdraw(bankAccount, amt))
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
                            Account recieverAccount;
                            Console.WriteLine(Constants.Messages.ServiceChargeType);
                            string choice = Console.ReadLine();
                            Console.WriteLine(Constants.Messages.ReceiverAccountId);
                            string recieverAccountId = Console.ReadLine();
                            try
                            {
                                recieverAccount = commonServices.FindAccount(recieverAccountId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                goto CustomerOperations;
                            }
                            if (recieverAccount != null)
                            {
                                Console.WriteLine(Constants.Messages.Amount);
                                double amtToTransfer = Convert.ToDouble(Console.ReadLine());
                                if (accountHolder.Transfer(bankAccount, amtToTransfer, recieverAccount, choice))
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
                            Console.WriteLine(Constants.Messages.TransactionHistoryChoice);
                            string choice = Console.ReadLine();
                            if (choice == "1")
                            {
                                ConsoleOutput.TransactionHistory();
                                commonServices.TransactionHistory(bankAccount);
                            }
                            else if (choice == "2")
                            {
                                commonServices.WriteHistory(bankAccount);
                            }
                            else
                            {
                                Console.Clear();
                                ConsoleOutput.InValidOption();
                            }
                        }
                        else if (customerOperation == CustomerOperationType.Balance)
                        {
                            Console.Clear();
                            ConsoleOutput.Balance();
                            Console.Write(accountHolder.ViewBalance(bankAccount));
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

