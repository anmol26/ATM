
namespace ATM.Models
{
    public class Staff : User
    {
        public Staff(string name, long number, string password, string gender)
        {
            this.Name = name;
            this.PhoneNumber = number;
            this.Password = password;
            this.Gender = gender;
            this.Id = GenerateAccountId(name);
        }
    }
}
