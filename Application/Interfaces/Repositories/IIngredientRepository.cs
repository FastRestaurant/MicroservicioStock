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
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(Ingredient ingredient);

        Task<Ingredient?> GetByNameAsync(string name);
    }
}
