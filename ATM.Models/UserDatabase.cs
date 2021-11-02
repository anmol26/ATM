using System.Collections.Generic;

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
