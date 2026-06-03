using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Queries;
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
                return (new  List<IngredientResponseDTO>(), "No hay ingredientes");

            var ingredientsDTO = ingredients.Select(ingredientEntity => new IngredientResponseDTO
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name
            }).ToList();
            return (ingredientsDTO, "OK");
        }
    }
}
