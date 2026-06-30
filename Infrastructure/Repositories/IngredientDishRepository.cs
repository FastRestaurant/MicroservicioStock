using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IngredientDishRepository : Application.Interfaces.Repositories.IIngredientDishRepository
    {
        private readonly AppDbContext _context;

        public IngredientDishRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IngredientDish ingredientDish)
        {
            await _context.IngredientDish.AddAsync(ingredientDish);
        }

        public Task DeleteAsync(IngredientDish ingredientDish)
        {
            _context.IngredientDish.Remove(ingredientDish);
            return Task.CompletedTask;
        }

        public async Task<List<IngredientDish>> GetAllAsync()
        {
            return await _context.IngredientDish
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IngredientDish?> GetByIdAsync(Guid id)
        {
            return await _context.IngredientDish
                .Include(x => x.Ingredient)
                .FirstOrDefaultAsync(x => x.IdIngredientDish == id);
        }

        public async Task<List<IngredientDish>> GetByDishIdAsync(Guid dishId)
        {
            return await _context.IngredientDish
                .AsNoTracking()
                .Where(x => x.Id_Dish == dishId)
                .ToListAsync();
        }

        public Task UpdateAsync(IngredientDish ingredientDish, byte[] rowVersion)
        {
            _context.Entry(ingredientDish)
                .Property(i => i.RowVersion)
                .OriginalValue = rowVersion;

            _context.IngredientDish.Update(ingredientDish);
            return Task.CompletedTask;
        }

        public async Task ReplaceByDishIdAsync(Guid dishId, IReadOnlyCollection<IngredientDish> ingredientDishes)
        {
            var currentItems = await _context.IngredientDish
                .Where(x => x.Id_Dish == dishId)
                .ToListAsync();

            _context.IngredientDish.RemoveRange(currentItems);
            await _context.IngredientDish.AddRangeAsync(ingredientDishes);
        }
    }
}

