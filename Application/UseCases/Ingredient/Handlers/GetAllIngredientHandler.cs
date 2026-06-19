using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Queries;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            if( ingredients == null || ingredients.Count == 0)
                throw new NotFoundException("No hay ingredientes");

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
