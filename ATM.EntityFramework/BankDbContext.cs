using ATM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.EntityFramework
{
    public class BankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Data Source = ANMOL\SQLEXPRESS; Initial Catalog = ATM; Integrated Security = True");
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(m => m.Id);

                entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(30);

                entity.Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(12)
                ;
                entity.Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(12)
                ;
                entity.Property(m => m.Balance)
                .IsRequired()
                ;
                entity.Property(m => m.IsActive);
                entity.Property(m => m.Gender);
                entity.Property(m => m.CurrentDate);
                entity.Property(m => m.BankId);

            });


        }
    }
}
