using System;
using System.Data.SqlClient;
using ATM.Models;

namespace ATM.Services
{
    public class CustomerService
    {
        Bank bank;
        const string DefaultCurrency = "INR";
        readonly CommonServices commonServices = new CommonServices();
        public void Deposit(SqlConnection conn, Account user, double amount, string currCode, string bankId)
        {
            try
            {
                user.Balance += Math.Round(amount * (double)(Currency.curr[currCode] / Currency.curr[DefaultCurrency]), 2);
                Console.WriteLine("Updated Balance is: "+user.Balance);
                string queryy = $"Update Account set Balance=N'{user.Balance}' where id=N'{user.Id}' ";
                SqlCommand commandd = new SqlCommand(queryy, conn);
                commandd.ExecuteNonQuery();

                Transaction trans = new Transaction(amount, 1, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                user.Transactions.Add(trans);
                string type = "Credit";
                string query = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                $" VALUES(N'{commonServices.GenerateTransactionId(bankId,user.Id)}',N'{user.Id}',N'{user.Id}',N'{bankId}',N'{bankId}'," +
                $"N'{type}',N'{DateTime.Now}',N'{amount}')";
                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
                Library.TransactionList.Add(trans);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Withdraw(SqlConnection conn, Account user, double amount, string bankId)
        {
            try
            {
                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                    Console.WriteLine("Updated Balance is: " + user.Balance);
                    string queryy = $"Update Account set Balance=N'{user.Balance}' where id=N'{user.Id}' ";
                    SqlCommand commandd = new SqlCommand(queryy, conn);
                    commandd.ExecuteNonQuery();

                    Transaction trans = new Transaction(amount, 2, user.Id, user.Id, bankId, bankId, commonServices.GenerateTransactionId(bankId, user.Id));
                    user.Transactions.Add(trans);
                    string type = "Debit";
                    string query = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                    $" VALUES(N'{commonServices.GenerateTransactionId(bankId, user.Id)}',N'{user.Id}',N'{user.Id}',N'{bankId}',N'{bankId}'," +
                    $"N'{type}',N'{DateTime.Now}',N'{amount}')";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    Library.TransactionList.Add(trans);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public bool Transfer(SqlConnection conn, Account sender, double amt, Account rcvr, string fromid, string toid, string choice)
        {
            Bank reciever = null;
            try
            {
                foreach (var i in Library.BankList)
                {
                    if (i.Id == fromid)
                    {
                        bank = i;
                    }
                    if (i.Id == toid)
                    {
                        reciever = i;
                    }
                }
                double charge;
                if (fromid == toid)
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, bank.SameRTGS);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.SameIMPS);
                    }

                }
                else
                {
                    if (choice == "1")
                    {
                        charge = DeductCharges(amt, bank.DiffRTGS);
                    }
                    else
                    {
                        charge = DeductCharges(amt, bank.DiffIMPS);
                    }
                }
                if (sender.Balance >= amt + charge)
                {
                    sender.Balance -= amt + charge;
                    Console.WriteLine("Updated Balance is: " + sender.Balance);
                    string queryy = $"Update Account set Balance=N'{sender.Balance}' where id=N'{sender.Id}' ";
                    SqlCommand commandd = new SqlCommand(queryy, conn);
                    commandd.ExecuteNonQuery();


                    rcvr.Balance += Math.Round(amt * (double)(Currency.curr[bank.CurrencyCode] / Currency.curr[reciever.CurrencyCode]), 2);
                    Console.WriteLine("Updated Balance is: " + rcvr.Balance);
                    string query = $"Update Account set Balance=N'{rcvr.Balance}' where id=N'{rcvr.Id}' ";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();

                    Transaction senderTrans = new Transaction(amt, 2, sender.Id, rcvr.Id, fromid, toid, commonServices.GenerateTransactionId(fromid, sender.Id));
                    sender.Transactions.Add(senderTrans);
                    string type = "Debit";
                    string query1 = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                    $" VALUES(N'{commonServices.GenerateTransactionId(fromid, sender.Id)}',N'{sender.Id}',N'{rcvr.Id}',N'{fromid}',N'{toid}'," +
                    $"N'{type}',N'{DateTime.Now}',N'{amt + charge}')";
                    SqlCommand command1 = new SqlCommand(query1, conn);
                    command1.ExecuteNonQuery();
                    Library.TransactionList.Add(senderTrans);

                    Transaction rcvrTrans = new Transaction(amt, 1, sender.Id, rcvr.Id, fromid, toid, commonServices.GenerateTransactionId(toid, rcvr.Id));
                    rcvr.Transactions.Add(rcvrTrans);
                    string type2 = "Credit";
                    string query2 = $"INSERT INTO [ATM].[dbo].[Transaction]" +
                    $" VALUES(N'{commonServices.GenerateTransactionId(toid, rcvr.Id)}',N'{sender.Id}',N'{rcvr.Id}',N'{fromid}',N'{toid}'," +
                    $"N'{type2}',N'{DateTime.Now}',N'{amt}')";
                    SqlCommand command2 = new SqlCommand(query2, conn);
                    command2.ExecuteNonQuery();
                    Library.TransactionList.Add(rcvrTrans);

                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Transfer: {0}", ex.Message);
            }
            return false;

        }
        public double DeductCharges(double amount, double percent)
        {
            return (double)Math.Round(amount * (Convert.ToDouble(percent) / 100), 2);
        }
        public double ViewBalance(Account user)
        {
            return user.Balance;
        }
    }
}
