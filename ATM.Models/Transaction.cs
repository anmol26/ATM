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
            Console.WriteLine("\n\t\tTransaction History:--");
            Console.WriteLine("\t\t<--------*-----*------->\n");
            int counter = 1;
            foreach (string transaction in Transaction.Transactions)
            {
                Console.WriteLine($"{counter}-> {transaction}");
                counter += 1;

            }

        }

    }
}
