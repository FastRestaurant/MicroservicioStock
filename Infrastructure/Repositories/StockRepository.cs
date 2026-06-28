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

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stock
                .AsNoTracking()
                .ToListAsync();
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
