using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class UserDatabase
    {
        public static Dictionary<string, string> AdminUsers = new Dictionary<string, string>
        {
                { "Vijay", "1234" },                                              //registered users

        };
        public static Dictionary<string, string> StaffUsers = new Dictionary<string, string>
        {
                { "Koni", "1234" },                                              //registered users
                { "Sagar", "0000" }                                              //registered users

        };
        public static Dictionary<string, string> AccountUsers = new Dictionary<string, string>
        {
                { "Anmol", "1234" },                                              //registered users
                { "Balaji", "0000" }                                              //registered users

        };
    }
}
