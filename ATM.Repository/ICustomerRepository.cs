using ATM.Models;

namespace ATM.Repository
{
    public interface ICustomerRepository
    {
        void UpdateBalance(string id, double balance);
        void InsertTransaction(Transaction trans);
        double FindExchangeRate(string currCode);
    }
}
