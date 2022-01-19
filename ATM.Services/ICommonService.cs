using ATM.Models;
namespace ATM.Services
{
    public interface ICommonService
    {
        dynamic UserLogin(string userId, string pass, string choice);
        string GenerateBankId(string bankName);
        string GenerateAccountId(string accName);
        string GenerateTransactionId(string bankId, string accountId);
        Bank FindBank(string bankId);
        Account FindAccount(string userId);
        void WriteHistory(Account bankAccount);
        void TransactionHistory(Account bankAccount);
    }
}
