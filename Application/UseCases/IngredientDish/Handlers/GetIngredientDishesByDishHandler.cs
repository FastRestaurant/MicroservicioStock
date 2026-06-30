using Application.DTOs.IngredientDishDTO;
using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Queries;
using Domain.Exceptions;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class GetIngredientDishesByDishHandler : IGetIngredientDishesByDishHandler
    {
        private readonly IIngredientDishRepository _ingredientDishRepository;

        public GetIngredientDishesByDishHandler(IIngredientDishRepository ingredientDishRepository)
        {
            _ingredientDishRepository = ingredientDishRepository;
        }

        public async Task<List<IngredientDishResponseDTO>> Handle(GetIngredientDishesByDishQuery query)
        {
            if (query.DishId == Guid.Empty)
                throw new ValidationException("El id del plato es obligatorio");

            var items = await _ingredientDishRepository.GetByDishIdAsync(query.DishId);
            return items.Select(item => new IngredientDishResponseDTO
            {
                IdIngredientDish = item.IdIngredientDish,
                Id_Ingredient = item.Id_Ingredient,
                Id_Dish = item.Id_Dish,
                RequiredQuantity = item.RequiredQuantity,
                RowVersion = Convert.ToBase64String(item.RowVersion)
            }).ToList();
        }
    }
}
