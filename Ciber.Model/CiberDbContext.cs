using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ciber.Model
{
    public class CiberDbContext : DbContext
    {
        public CiberDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        //protected IConfiguration Configs { get; set; }

        //public const string ConnectStrring = @"Data Source=DESKTOP-3GHMQUF\\SQLEXPRESS;Initial Catalog=Ciber;Integrated Security=True";
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConnectStrring);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(c => c.Id);
        }
    }
   
}
