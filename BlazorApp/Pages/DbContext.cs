using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Pages
{
    public class AppDbContext : DbContext
    {
        public DbSet<Seller> Users { get; set; }
        public DbSet<Hardware> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Local;");
        }
    }
}
