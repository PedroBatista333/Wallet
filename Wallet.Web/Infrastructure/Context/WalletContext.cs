using System.Data.Entity;
using Wallet.Web.Models;

namespace Wallet.Web.Infrastructure.Context
{
    public class WalletContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public WalletContext(string connectionString) :
            base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>().ToTable("Income");
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Properties<string>()
                .Configure(s => s.HasColumnType("varchar"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
