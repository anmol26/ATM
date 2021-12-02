using ATM.Models;
using Microsoft.EntityFrameworkCore;
using ATM.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Repository
{
    public partial class BankDbContext : DbContext
    {
        public BankDbContext()
        {
        }

        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDb> Accounts { get; set; }
        public virtual DbSet<BankDb> Banks { get; set; }
        public virtual DbSet<CurrencyDb> Currencies { get; set; }
        public virtual DbSet<TransactionDb> Transactions { get; set; }
        public virtual DbSet<StaffDb> Staffs { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= ANMOL\\SQLEXPRESS;Database=ATM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountDb>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => new { e.Name, e.BankId }, "Unique_Account")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(15)
                    .HasColumnName("id");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.BankId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CurrentDate).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<BankDb>(entity =>
            {
                entity.ToTable("Bank");

                entity.HasIndex(e => e.Name, "UQ__Bank__737584F6ECD50295")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(15)
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Branch)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength(true);

                entity.Property(e => e.DiffImps)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("DiffIMPS");

                entity.Property(e => e.DiffRtgs)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("DiffRTGS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SameImps)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("SameIMPS");

                entity.Property(e => e.SameRtgs)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("SameRTGS");
            });

            modelBuilder.Entity<CurrencyDb>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TransactionDb>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CurrentDate).HasMaxLength(50);

                entity.Property(e => e.RecieverAccountId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.RecieverBankId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.SenderAcountId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.SenderBankId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<StaffDb>(entity =>
            {
                entity.ToTable("Staff");

                entity.HasIndex(e => new { e.Name, e.BankId }, "Unique_Person")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(15)
                    .HasColumnName("id");

                entity.Property(e => e.BankId)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CurrentDate).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }



    //PM > Scaffold - DbContext "Server= ANMOL\SQLEXPRESS;Database=ATM;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models


}
