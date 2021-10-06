using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class User
    {

        public string userName;

        public string accountNum;

        public static Dictionary<string, string> Users = new Dictionary<string, string>
        {
                { "Anmol", "1234" },                                              //registered users
                { "Balaji", "0000" }                                              //registered users
        };

    }

}
