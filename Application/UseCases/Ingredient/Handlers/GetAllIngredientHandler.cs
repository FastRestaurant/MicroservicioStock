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

        public async Task<Application.DTOs.PagedResponseDTO<IngredientResponseDTO>> Handle(GetAllIngredientsQuery query)
        {
            var ingredients = await _IngredientRepository.GetAllAsync(query.Page, query.PageSize);
            var totalItems = await _IngredientRepository.CountAsync();

            var ingredientsDTO = ingredients.Select(ingredientEntity => new IngredientResponseDTO
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name,
                StockId = ingredientEntity.Id_Stock,
                StockCount = ingredientEntity.Stock?.Count ?? 0
            }).ToList();
            return new Application.DTOs.PagedResponseDTO<IngredientResponseDTO>
            {
                Items = ingredientsDTO,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize)
            };
        }
    }
}
