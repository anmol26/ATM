﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public class Currency
    {
        public static Dictionary<string, double> curr = new Dictionary<string, double>()
        {
            { "EUR", 0.9},
            { "INR", 74.1},
            { "USD", 1.0}
        };
    }
    
}
