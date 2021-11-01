using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class UserDatabase
    {
        public static List<Bank> Banks = new List<Bank>();

        public static Dictionary<string, string> AdminUsers = new Dictionary<string, string>
        {
                { "Vijay", "1234" },                                              //registered users

        };
        
    }
}
