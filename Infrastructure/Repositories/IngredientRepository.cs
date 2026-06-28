using Application.Interfaces.Repositories;
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
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            await _context.Ingredient.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ingredient ingredient)
        {
            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Ingredient
                .AsNoTracking()
                .Include(i => i.Stock)
                .OrderBy(i => i.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Ingredient.CountAsync();
        }

        public async Task<Ingredient?> GetByIdAsync(Guid id)
        {
            return await _context.Ingredient
                .Include(i => i.Stock)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Ingredient?> GetByNameAsync(string name)
        {
            return await _context.Ingredient
                .Include(i => i.Stock)
                .FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _context.Ingredient.Update(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
