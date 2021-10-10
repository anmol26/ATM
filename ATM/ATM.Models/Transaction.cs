using System;
using System.Collections.Generic;
using System.Text;
using ATM.Services;

namespace ATM.Models
{
    public class Transaction
    {

        public static List<string> Transactions = new List<string>();

        public static void ShowTransactions()
        {
            Message.TransactionHistory();
            int counter = 1;
            foreach (string transaction in Transaction.Transactions)
            {
                Console.WriteLine($"{counter}. {transaction}");
                counter += 1;

            }

        }

    }

}

