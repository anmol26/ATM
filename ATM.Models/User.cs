using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string BankId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CurrentDate { get; set; }
        
    }
}
