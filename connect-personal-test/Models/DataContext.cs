using connect_personal_test.Models.Storage;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace connect_personal_test.Models
{
    /// <summary>
    /// Контекст данных.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Заказы.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Категории.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Связь заказа с категориями.
        /// </summary>
        public DbSet<OrderCategory> OrderCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("IX_UQ_Categories_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<OrderCategory>(entity =>
            {
                entity.HasKey(c => new { c.OrderId, c.CategoryId });
            });
        }
    }
}
