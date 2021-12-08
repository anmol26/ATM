using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATM.Repository.Models
{
    public class ATMContext : DbContext
    {
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<BankDb> Banks { get; set; }
        public DbSet<CurrencyDb> Currencies { get; set; }
        public DbSet<TransactionDb> Transactions { get; set; }
        public DbSet<StaffDb> Staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= ANMOL\\SQLEXPRESS;Database=Banking;Trusted_Connection=True;");
            }
        }
    }
}