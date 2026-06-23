using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Queries;

namespace Application.UseCases.Ingredient.Handlers
{
    public class GetAllIngredientHandler : IGetAllIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public GetAllIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<(List<IngredientResponseDTO> ingredientsList, string message)> Handle(GetAllIngredientsQuery query)
        {
            var ingredients = await _IngredientRepository.GetAllAsync();

            var ingredientsDTO = ingredients.Select(ingredientEntity => new IngredientResponseDTO
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name,
                StockId = ingredientEntity.Id_Stock,
                StockCount = ingredientEntity.Stock?.Count ?? 0
            }).ToList();
            return (ingredientsDTO, "OK");
        }
    }
}
