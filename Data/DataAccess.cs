using KhumaloCraft.Model;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Data
{
    public class DataAccess : DbContext
    {
        public DataAccess(DbContextOptions<DataAccess> options) : base(options) { }
        public DbSet<User> User {get; set;}
        public DbSet<Product> Product { get; set;}
        public DbSet<Transaction> Transaction { get; set;}
    }
}
