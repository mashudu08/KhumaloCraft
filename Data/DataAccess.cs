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

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users {get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Cart> Carts { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
