using ATM.Models;

namespace ATM.Repository
{
    public interface IStaffRepository
    {
        void InsertNewBank(Bank bank, Staff s);
        void InsertNewStaff(Staff s);
        void InsertNewAccount(Account a);
        void DeleteAccount(string userId);
        void DeleteStaff(string staffId);
        void DeleteBank(string bankId);
        void InsertNewCurrency(string code, double rate);
        void UpdateCharges(string bankId, double rtgs, double imps, int choice);
        void UpdateBalance(string accountId, double balance);
        void UpdateAccount(string accountId, string? name, long? phoneNumber, string? password);


    }
}
