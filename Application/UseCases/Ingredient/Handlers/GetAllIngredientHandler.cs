using Application.DTOs;
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

        public async Task<PagedResponseDTO<IngredientResponseDTO>> Handle(GetAllIngredientsQuery query)
        {
            var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
            var pageSize = query.PageSize < 1 ? 10 : query.PageSize;
            if (pageSize > 50) pageSize = 50;

            var (ingredients, totalCount, currentPage) = await _IngredientRepository.GetPageAsync(pageNumber, pageSize, query.Search);

            var ingredientsDTO = ingredients.Select(ingredientEntity => new IngredientResponseDTO
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name,
                StockId = ingredientEntity.Id_Stock,
                StockCount = ingredientEntity.Stock?.Count ?? 0,
                UnitType = ingredientEntity.UnitType
            }).ToList();

            return new PagedResponseDTO<IngredientResponseDTO>
            {
                Items = ingredientsDTO,
                Page = currentPage,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
