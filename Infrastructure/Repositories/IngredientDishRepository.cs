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
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IngredientDish ingredientDish)
        {
            _context.IngredientDish.Remove(ingredientDish);
            await _context.SaveChangesAsync();
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

        public async Task UpdateAsync(IngredientDish ingredientDish)
        {
            _context.IngredientDish.Update(ingredientDish);
            await _context.SaveChangesAsync();
        }
    }
}

