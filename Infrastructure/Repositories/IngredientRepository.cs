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

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredient
                .AsNoTracking()
                .Include(i => i.Stock)
                .ToListAsync();
        }

        public async Task<(List<Ingredient> Items, int TotalCount, int PageNumber)> GetPageAsync(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Ingredient
                .AsNoTracking()
                .Include(i => i.Stock)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(i => i.Name.Contains(term));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages > 0 && pageNumber > totalPages)
                pageNumber = totalPages;

            var items = await query
                .OrderBy(i => i.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount, pageNumber);
        }

        public async Task<Ingredient?> GetByIdAsync(Guid id)
        {
            return await _context.Ingredient
                .Include(i => i.Stock)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Ingredient>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var ingredientIds = ids.Distinct().ToArray();

            return await _context.Ingredient
                .AsNoTracking()
                .Where(i => ingredientIds.Contains(i.Id))
                .ToListAsync();
        }

        public async Task<Ingredient?> GetByNameAsync(string name)
        {
            return await _context.Ingredient
                .Include(i => i.Stock)
                .FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task UpdateAsync(Ingredient ingredient, byte[] rowVersion)
        {
            _context.Entry(ingredient)
                .Property(i => i.RowVersion)
                .OriginalValue = rowVersion;

            _context.Ingredient.Update(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
