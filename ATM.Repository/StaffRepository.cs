using System;
using ATM.Models;
using System.Data.SqlClient;
using System.Linq;

namespace ATM.Repository
{
    public class StaffRepository
    {
        readonly Library lib = new Library();
        public void InsertNewBank(string bankId, Bank bank, string name, string address, string branch, string currencyCode)
        {
            try
            {
                lib.GetBankList().Add(bank);
                string query = $"INSERT INTO Bank" +
                    $" VALUES(N'{bankId}',N'{name}',N'{address}',N'{branch}',N'{currencyCode}'," +
                    $"N'{bank.SameRTGS}',N'{bank.SameIMPS}',N'{bank.DiffRTGS}',N'{bank.DiffIMPS}')";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertNewStaff(Staff s, string staffId, string name, string password, long phoneNumber, string gender, string bankId)
        {
            try
            {
                lib.GetStaffList().Add(s);
                string query = $"INSERT INTO Staff" +
               $" VALUES(N'{staffId}',N'{name}',N'{password}',N'{phoneNumber}',1," +
               $"N'{DateTime.Now}',N'{gender}',N'{bankId}')";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                int counter = 0;
                foreach (var staff in lib.GetStaffList().Where(staff => staff.BankId == bankId))
                {
                    counter += 1;
                }
                if (counter == 0)
                {
                    DeleteBank(bankId);
                }
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewAccount(Account a, string accountId, string name, string password, long phoneNumber, string gender, string bankId)
        {
            try
            {
                lib.GetAccountHolderList().Add(a);
                string query = $"INSERT INTO Account" +
               $" VALUES(N'{accountId}',N'{name}',N'{password}',N'{phoneNumber}',0,1," +
               $"N'{gender}',N'{DateTime.Now}',N'{bankId}')";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteAccount(Account user)
        {
            try
            {
                lib.GetAccountHolderList().Remove(user);
                string query = $"Delete from Account where id=N'{user.Id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertNewCurrency(string code, double rate)
        {
            try
            {
                string query = $"Insert into Currency Values(N'{code}',N'{rate}') ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCharges(string bankId, double rtgs, double imps, int choice)
        {
            try
            {
                if (choice == 1)
                {
                    string query1 = $"Update Bank set SameRTGS=N'{rtgs}', SameIMPS=N'{imps}' where id=N'{bankId}' ";
                    SqlCommand command = new SqlCommand(query1, DatabaseConnection.ConnectDatabase());
                    command.ExecuteNonQuery();
                }
                else if (choice == 2)
                {
                    string query2 = $"Update Bank set DiffRTGS=N'{rtgs}', DiffIMPS=N'{imps}' where id=N'{bankId}' ";
                    SqlCommand command = new SqlCommand(query2, DatabaseConnection.ConnectDatabase());
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateBalance(string id, double balance)
        {
            try
            {
                string query = $"Update Account set Balance=N'{balance}' where id=N'{id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateName(Account bankAccount, string name)
        {
            try
            {
                string query = $"Update Account set Name=N'{name}' where id=N'{bankAccount.Id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatePhoneNumber(Account bankAccount, long number)
        {
            try
            {
                string query = $"Update Account set PhoneNumber=N'{number}' where id=N'{bankAccount.Id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatePassword(Account bankAccount, string password)
        {
            try
            {
                string query = $"Update Account set Password=N'{password}' where id=N'{bankAccount.Id}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteBank(string bankId)
        {
            try
            {
                string query = $"Delete from Bank where id=N'{bankId}' ";
                SqlCommand command = new SqlCommand(query, DatabaseConnection.ConnectDatabase());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
