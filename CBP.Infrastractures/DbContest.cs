using Customer_Balance_Paltform.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer_Balance_Paltform.CBP.Infrastractures;

public class DbCTPContest : DbContext
{
    public DbCTPContest(DbContextOptions<DbCTPContest> options)
        : base(options)
    {
    }

    // DbSet for Customers
    public DbSet<TCustomer> Customers { get; set; }

    // DbSet for Transactions
    public DbSet<TTransactions> Transactions { get; set; }
    
    public DbSet<TContactInfo> ContactInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TCustomer>().HasKey(e => e.CustomerID);
        modelBuilder.Entity<TTransactions>().HasKey(e => e.TransactionID);
        modelBuilder.Entity<TContactInfo>().HasKey(e => e.ContactId);
      
        modelBuilder.Entity<TCustomer>()
            .HasOne(c => c.ContactInfo)
            .WithOne(ci => ci.Customer)
            .HasForeignKey<TContactInfo>(ci => ci.CustomerId);

        modelBuilder.Entity<TTransactions>()
            .HasOne(t => t.Customer)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CustomerID);
    }
}