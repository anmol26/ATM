using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.CLI
{
    public class ConsoleInput
    {
        public static string Input()
        {
            return Console.ReadLine();
        }
        public static string UserName()
        {
            return GetInput(Constants.Messages.UserName);
        }
        public static string Password()
        {
            return GetInput(Constants.Messages.Password);
        }
        public static string RecieverName()
        {
            return GetInput(Constants.Messages.RecieverName);
        }
        public static string Amount()
        {
            return GetInput(Constants.Messages.Amount);
        }
        public static string BankName()
        {
            return GetInput(Constants.Messages.BankName);
        }
        private static string GetInput(string message)
        {
            Console.WriteLine("\n"+message+"\n");
            string result = Console.ReadLine();
            return result;
        }

    }
}
