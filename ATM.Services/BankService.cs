using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;
using ATM.Models.Exceptions;

namespace ATM.Services
{
    public class BankService
    {
        private readonly List<Account> accounts;
        private readonly List<Bank> banks;
        public BankService()
        {
            this.banks = new List<Bank>();
            this.accounts = new List<Account>();

        }
        Account account = new Account();
        Bank bank = new Bank();

        public void CreateBank(string name,string address)
        {
            Bank bank = new Bank
            {
                Id = GenerateBankId(name),
                Name = name,
                Address = address

            };
            
            this.banks.Add(bank); 
        } 
        private string GenerateBankId(string bankName)
        {
            // Axis-> Axs-> AXS.... 26-07-2001-> 2672001=>     AXS2672001
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToShortDateString();
            string bankId = bankName.Substring(0, 3).ToUpper() + date.Replace("-", "");
            return bankId;
        } 
        public void CreateAccount(string userId, string password)
        {
            UserDatabase.AccountUsers.Add(userId, password);
            account.Id = GenerateAccountId(userId);
        }

        private string GenerateAccountId(string accName)
        {
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToShortDateString();
            string accId = accName.Substring(0, 3).ToUpper() + date.Replace("-", "");
            return accId;
        }
        private string GenerateTransactionId(string bankId, string accountId)
        {
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString();
            string txnId = "TXN" + bankId + accountId + date.Replace("-", "");
            return txnId;
        }
        public void UpdateAcceptedCurrency() 
        {
            //bank.AcceptedCurrency=
        }
        public void UpdateServiceCharge()
        { 
            Console.WriteLine("Enter the updated RTGS charge to same bank");
            double rtgsSame = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the updated IMPS charge to same bank");
            double impsSame = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the updated RTGS charge to different bank");
            double rtgsDifferent = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the updated IMPS charge to different bank");
            double impsDifferent = Convert.ToDouble(Console.ReadLine());
            bank.RTGSChargeToSameBank = rtgsSame;
            bank.RTGSChargeToOtherBanks = rtgsDifferent;
            bank.IMPSChargeToSameBank = impsSame;
            bank.IMPSChargeToOtherBanks = impsDifferent;
        }
        public void Deposit(double amount)
        {
            account.Balance += amount;
            Transaction.Transactions.Add(GenerateTransactionId(bank.Id,account.Id), $"{amount} deposited in account successfully.");
        }
        public void Withdraw(double amount)
        {
            if (account.Balance < amount)
            {
                throw new BalanceInsufficientException();
            }
            else
            {
                account.Balance -= Convert.ToDouble(amount);
                Transaction.Transactions.Add(GenerateTransactionId(bank.Id, account.Id), $"{amount} withdrawn from the account successfully.");
            }
        }
        public void Transfer(string userName, double amount)
        {
            if (amount < account.Balance)
            {
                account.Balance -= Convert.ToDouble(amount);
                Transaction.Transactions.Add(GenerateTransactionId(bank.Id, account.Id), $"{amount} has been transferred to " + userName + "'s account successfully.");
            }
            else
            {
                throw new SenderBalanceInsufficientException();
            }
        }
        public double Balance()
        {
            return account.Balance;
        }
        public void ShowTransactions()
        {
            int counter = 1;
            foreach (var transaction in Transaction.Transactions)
            {
                Console.WriteLine($"{counter}-> {transaction.Key}: {transaction.Value}");
                counter += 1;
            }
        }
    }
}
