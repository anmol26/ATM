
#nullable disable

namespace ATM.Repository.Models
{
    public partial class StaffDb
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string CurrentDate { get; set; }
        public string Gender { get; set; }
        public string BankId { get; set; }
    }
}
