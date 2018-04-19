using Microsoft.EntityFrameworkCore;
namespace bankaccounts.Models{
    public class UserContext : DbContext{
        public UserContext(DbContextOptions<UserContext> options) : base(options) {}
        public DbSet<Transaction> Transactions {get; set;}
        public DbSet<User> Users {get; set;}
        
    }
}