using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Transaction
    {

        public static List<string> Transactions = new List<string>();

        public static void ShowTransactions()
        {
            Message.TransactionHistory();
            foreach (string transaction in Transaction.Transactions)
            {
                Console.WriteLine(transaction);

            }

        }

    }

}

