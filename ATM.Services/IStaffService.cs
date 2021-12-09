using ATM.Models;

namespace ATM.Services
{
    public interface IStaffService
    {
        public string CreateBank(string name, string address, string branch, string currencyCode, string sName, string sPass, long sPhone, string gender);
        public string CreateAccount(string bankId, string name, string password, long phoneNumber, string gender, int choice);
        public void DeleteAccount(string userId);
        public void AddCurrency(string code, double rate);
        public void UpdateCharges(string bankId, double rtgs, double imps, int choice);
        public void RevertTransaction(string transid);
        public void PrintList(Bank bank, int a);
        public void WriteAllAccountHistory(string bankId);
        public void UpdateName(Account bankAccount, string name);
        public void UpdatePhoneNumber(Account bankAccount, long phoneNumber);
        public void UpdatePassword(Account bankAccount, string password);
    }
}
