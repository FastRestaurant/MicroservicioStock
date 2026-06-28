using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Queries;
using Domain.Exceptions;

namespace Application.UseCases.Ingredient.Handlers
{
    public class GetByIdIngredientHandler : IGetByIdIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public GetByIdIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<IngredientResponseDTO> Handle(GetByIdIngredientQuery query)
        {
            if (query == null)
                throw new ValidationException("Datos inválidos");
            if (query.Id == Guid.Empty)
                throw new ValidationException("Id inválido");

            var ingredient = await _IngredientRepository.GetByIdAsync(query.Id);

            if (ingredient == null)
                throw new NotFoundException("Ingrediente no encontrado");

            return new IngredientResponseDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                StockId = ingredient.Id_Stock,
                StockCount = ingredient.Stock?.Count ?? 0,
                UnitType = ingredient.UnitType
            };
        }
    }
}
