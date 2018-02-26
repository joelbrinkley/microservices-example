using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AccountMgmtView.Data
{
    public class AccountMgmtViewContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AccountMgmtViewContext()
        {

        }

        public AccountMgmtViewContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customerBuilder = modelBuilder.Entity<Customer>().ToTable("Customer");
            customerBuilder.HasKey(x => x.Id);
            customerBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            customerBuilder.HasIndex(x => x.CustomerObjectId);
            customerBuilder.HasMany(x => x.Accounts).WithOne(x => x.Customer);

            var accountBuilder = modelBuilder.Entity<Account>().ToTable("Account");
            accountBuilder.HasKey(x => x.Id);
            accountBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            accountBuilder.HasIndex(x => x.AccountObjectId);
        }
    }
}
