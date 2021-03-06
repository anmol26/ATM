using System;

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
        public static string DeleteUserName()
        {
            return GetInput(Constants.Messages.DeleteUserName);
        }
        public static string RecieverName()
        {
            return GetInput(Constants.Messages.RecieverName);
        }
        public static string DepositAmount()
        {
            return GetInput(Constants.Messages.DepositAmount);   
        }
        public static string WithdrawAmount()
        {
            return GetInput(Constants.Messages.WithdrawAmount);
        }
        public static string Amount()
        {
            return GetInput(Constants.Messages.Amount);
        }
        public static string UpdateCurrency()
        {
            return GetInput(Constants.Messages.UpdateCurrency);
        }
        public static string InitializeAmount()
        {
            return GetInput(Constants.Messages.Amount);
        }
        public static string BankName()
        {
            return GetInput(Constants.Messages.BankName);
        }
        public static string Address()
        {
            return GetInput(Constants.Messages.Address);
        }
        private static string GetInput(string message)
        {
            Console.WriteLine("\n"+message+"\n");
            string result = Console.ReadLine();
            return result;
        }

    }
}
