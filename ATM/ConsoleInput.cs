using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.CLI
{
    public class ConsoleInput
    {
        public static string UserName()
        {
            return GetInput("Please Enter the username");
        }
        public static string RecieverName()
        {
            return GetInput("Please Enter the Reciever name");
        }
        public static string Amount()
        {
            return GetInput("Please Enter the Amount");
        }
        private static string GetInput(string message)
        {
            Console.WriteLine("\n"+message+"\n");
            string result = Console.ReadLine();
            return result;
        }
    }
}
