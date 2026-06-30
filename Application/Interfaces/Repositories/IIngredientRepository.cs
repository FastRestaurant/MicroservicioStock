using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IIngredientRepository
    {
        Task AddAsync(Ingredient ingredient);
        Task<Ingredient?> GetByIdAsync(Guid id);
        Task<List<Ingredient>> GetAllAsync();
        Task<List<Ingredient>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<(List<Ingredient> Items, int TotalCount, int PageNumber)> GetPageAsync(int pageNumber, int pageSize, string? search);
        Task UpdateAsync(Ingredient ingredient, byte[] rowVersion);
        Task DeleteAsync(Ingredient ingredient);

        Task<Ingredient?> GetByNameAsync(string name);
    }
}
