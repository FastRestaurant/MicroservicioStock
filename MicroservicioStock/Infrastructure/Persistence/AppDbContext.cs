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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Ingredient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.HasOne<Domain.Entities.Stock>(s => s.Stock)
                    .WithMany(g => g.Ingredients)
                    .HasForeignKey(s => s.Id_Stock)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Domain.Entities.Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();


            });
            modelBuilder.Entity<Domain.Entities.IngredientDish>(entity =>
            {
                entity.HasOne<Domain.Entities.Ingredient>(s => s.Ingredient)
                    .WithMany(g => g.IngredientDishes)
                    .HasForeignKey(s => s.Id_Ingredient)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            
        }
    }
}
