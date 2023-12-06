using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Pages
{
    public class AppDbContext : DbContext
    {
        //Samling af brugerobjekter i databasen
        public DbSet<Seller> Users { get; set; }
        //Samling af produkter i databasen
        public DbSet<Hardware> Products { get; set; }

        //Forbindelse til databasen
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fortæller det er en SQL Database
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Local;");
        }
    }
}
