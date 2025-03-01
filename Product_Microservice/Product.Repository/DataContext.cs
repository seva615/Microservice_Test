using Microsoft.EntityFrameworkCore;
using Product.Data.Entities;

namespace Product.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
           // Database.EnsureCreated();
        }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ImageEntity> Images { get; set; }

        public DbSet<CartEntity> Carts { get; set; }

        public DbSet<CartRecordEntity> CartRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasMany(e => e.ProductImages).WithOne(e => e.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartRecordEntity>().HasOne(e => e.Product).WithMany(e => e.CartRecords);
            modelBuilder.Entity<CartEntity>().HasMany(e => e.CartRecords).WithOne(e => e.Cart).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}