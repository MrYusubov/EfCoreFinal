using MarketApp.Models;
using Microsoft.EntityFrameworkCore;
namespace MarketApp.Data
{
    public class MarketAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=STHQ0118-05;Initial Catalog=MarketDb;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasMany(c => c.Products)
                        .WithOne(p => p.Category)
                        .HasForeignKey(p => p.CategId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany(p => p.Products)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.UserId);
                e.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(200);
                e.Property(u => u.Password)
                      .IsRequired();
            });

            modelBuilder.Entity<Cart>(e =>
            {
                e.HasKey(c => c.CartId);
                e.HasOne(c => c.User)
                    .WithMany() 
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(c => c.Items)
                    .WithOne()
                    .HasForeignKey(ci => ci.CartItemId);
            });

            modelBuilder.Entity<CartItem>(e =>
            {
                e.HasKey(ci => ci.CartItemId);
                e.HasOne(ci => ci.Product)
                      .WithMany()
                      .HasForeignKey(ci => ci.ProdId);
                e.Property(ci => ci.Quantity)
                      .IsRequired();
            });
        }
    }
}
