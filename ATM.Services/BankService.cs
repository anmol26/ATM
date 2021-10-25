using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

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


        public void CreateBank(string name)
        {
            Bank bank = new Bank
            {
                Id = GenerateBankId(name),
                Name = name
            };
            this.banks.Add(bank);
        }
        private string GenerateBankId(string bankName)
        {

            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToShortDateString();
            string bankId = bankName.Substring(0, 3).ToUpper() + date.Replace("-", "");
            return bankId;
        }


        public void CreateAccount(string userId, string password, double initializeAmount)
        {
            Account.Users.Add(userId, password);
            account.Balance = initializeAmount;
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
            string date = currentDate.ToShortDateString();
            string txnId = "TXN" + bankId + accountId + date.Replace("-", "");
            return txnId;

        }
        public void Deposit(double amount)
        {
            account.Balance += amount;
            Transaction.Transactions.Add($"{amount} deposited in account successfully.");

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
                Transaction.Transactions.Add($"{amount} withdrawn from the account successfully.");
            }

        }
        public void Transfer(string userName, double amount)
        {
            if (amount < account.Balance)
            {
                Withdraw(amount);
                Transaction.Transactions.Add($"{amount} has been transferred to " + userName + "'s account successfully.");
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
            foreach (string transaction in Transaction.Transactions)
            {
                Console.WriteLine($"{counter}-> {transaction}");
                counter += 1;

            }

        }

    }
}
