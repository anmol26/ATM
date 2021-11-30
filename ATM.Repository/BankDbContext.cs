using ATM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Repository
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
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("[ATM].[dbo].[Bank]");
                entity.Property(m => m.Id);

                entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(30);

                entity.Property(m => m.Address)
                .IsRequired()
                ;
                entity.Property(m => m.Branch)
                .IsRequired()
                ;
                entity.Property(m => m.CurrencyCode)
                .IsRequired()
                ;
                entity.Property(m => m.SameRTGS)
                ;
                entity.Property(m => m.SameIMPS)
                ;
                entity.Property(m => m.DiffRTGS)
                ;
                entity.Property(m => m.DiffIMPS)
                ;
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
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
                entity.Property(m => m.IsActive)
                .IsRequired()
                ;
                entity.Property(m => m.Gender)
                .IsRequired()
                ;
                entity.Property(m => m.CurrentDate)
                .IsRequired()
                ;
                entity.Property(m => m.BankId)
                .IsRequired()
                ;
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");
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
                entity.Property(m => m.IsActive)
                .IsRequired()
                ;
                entity.Property(m => m.CurrentDate)
                .IsRequired()
                ;
                entity.Property(m => m.Gender)
                .IsRequired()
                ;
                entity.Property(m => m.BankId)
                .IsRequired()
                ;
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.Property(m => m.Id);

                entity.Property(m => m.SenderAccountId)
                .IsRequired();

                entity.Property(m => m.RecieverAccountId)
                .IsRequired()
                ;
                entity.Property(m => m.SenderBankId)
                .IsRequired()
                ;
                entity.Property(m => m.RecieverBankId)
                .IsRequired()
                ;
                entity.Property(m => m.Type)
                .IsRequired()
                ;
                entity.Property(m => m.CurrentDate)
                .IsRequired()
                ;
                entity.Property(m => m.Amount)
                .IsRequired()
                ;
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");
                entity.Property(m => m.CurrencyCode)
                .IsRequired();

                entity.Property(m => m.Exchangerate)
                .IsRequired();
            });

        }
    }
}
