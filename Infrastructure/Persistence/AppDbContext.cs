using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Domain.Entities.Ingredient> Ingredient { get; set; }
        public DbSet<Domain.Entities.Stock> Stock { get; set; }
        public DbSet<Domain.Entities.IngredientDish> IngredientDish { get; set; }
        public DbSet<Domain.Entities.StockMovement> StockMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Ingredient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.Name).IsRequired().HasMaxLength(150);
                entity.Property(t => t.UnitType).HasConversion<string>().IsRequired();
                entity.HasIndex(t => t.Name).IsUnique();

                entity.HasOne<Domain.Entities.Stock>(s => s.Stock)
                    .WithMany(g => g.Ingredients)
                    .HasForeignKey(s => s.Id_Stock)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Domain.Entities.Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.Property(t => t.Count).HasPrecision(18, 3);
                entity.HasIndex(t => t.Id_Drink)
                    .IsUnique();
            });
            modelBuilder.Entity<Domain.Entities.IngredientDish>(entity =>
            {

                entity.HasKey(x => x.IdIngredientDish);
                entity.Property(x => x.IdIngredientDish).ValueGeneratedOnAdd();
                entity.Property(x => x.RequiredQuantity).HasPrecision(18, 3);
                entity.HasIndex(x => new { x.Id_Dish, x.Id_Ingredient }).IsUnique();

                entity.HasOne(x => x.Ingredient)
                    .WithMany(x => x.IngredientDishes)
                    .HasForeignKey(x => x.Id_Ingredient)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Domain.Entities.StockMovement>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.ProductType).IsRequired().HasMaxLength(20);
                entity.Property(x => x.MovementType).IsRequired().HasMaxLength(20);
                entity.Property(x => x.Quantity).HasPrecision(18, 3);
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.HasIndex(x => new { x.OrderItemId, x.StockId, x.MovementType }).IsUnique();
                entity.HasIndex(x => new { x.OrderId, x.OrderItemId });

                entity.HasOne(x => x.Stock)
                    .WithMany(x => x.Movements)
                    .HasForeignKey(x => x.StockId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
