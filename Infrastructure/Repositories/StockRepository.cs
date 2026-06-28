using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _context.Stock
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Stock>> GetAllAsync(int page, int pageSize, bool onlyDrinks = false)
        {
            var query = _context.Stock.AsNoTracking();

            if (onlyDrinks)
                query = query.Where(stock => stock.Id_Drink != null);

            return await query
                .OrderBy(stock => stock.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync(bool onlyDrinks = false)
        {
            return onlyDrinks
                ? await _context.Stock.CountAsync(stock => stock.Id_Drink != null)
                : await _context.Stock.CountAsync();
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stock.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if (stock != null)
            {
                _context.Stock.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Stock?> GetByDrinkIdAsync(Guid drinkId)
        {
            return await _context.Stock
                .FirstOrDefaultAsync(s => s.Id_Drink == drinkId);
        }
    }
}
