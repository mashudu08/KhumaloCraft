using KhumaloCraft.enums;
using KhumaloCraft.Model;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Data
{
    public class DataAccess : DbContext
    {
        public DataAccess(DbContextOptions<DataAccess> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure enum mapping for Role property
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),    // Convert enum to string for database storage
                    v => (Role)Enum.Parse(typeof(Role), v)); // Convert string from database to enum

            // Other entity configurations...

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users {get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<Transaction> Transactions { get; set;}
    }
}
