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
            return GetInput("\nPlease Enter the username");
        }
        public static string Password()
        {
            return GetInput("\nPlease Enter the Password");
        }
        public static string RecieverName()
        {
            return GetInput("\nPlease Enter the Reciever name");
        }
        public static string Amount()
        {
            return GetInput("\nPlease Enter the Amount");
        }
        private static string GetInput(string message)
        {
            Console.WriteLine("\n"+message+"\n");
            string result = Console.ReadLine();
            return result;
        }

    }
}
