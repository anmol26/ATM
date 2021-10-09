using System;
using System.Collections.Generic;
using System.Text;
using ATM.Models;

namespace ATM.Services
{
    public class InsufficientMoneyException : Exception
    {
        public InsufficientMoneyException(string message) : base(message) 
        { 
        }
    }
}
