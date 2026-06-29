using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IIngredientDishRepository
    {
        Task AddAsync(IngredientDish ingredientDish);
        Task<IngredientDish?> GetByIdAsync(Guid id);
        Task<List<IngredientDish>> GetByDishIdAsync(Guid dishId);
        Task<List<IngredientDish>> GetAllAsync();
        Task DeleteAsync(IngredientDish ingredientDish);
        Task UpdateAsync(IngredientDish ingredientDish);
        Task ReplaceByDishIdAsync(Guid dishId, IReadOnlyCollection<IngredientDish> ingredientDishes);

    }
}
