using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Handlers
{
    public class GetByIdIngredientHandler : IGetByIdIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public GetByIdIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<(IngredientResponseDTO ingredient, string message)> Handle(GetByIdIngredientQuery query)
        {
            if(query == null)
                return (new IngredientResponseDTO(), "Datos inválidos");
            if (query.Id == Guid.Empty)
                return (new IngredientResponseDTO(), "Id inválido");

            var ingredient = await _IngredientRepository.GetByIdAsync(query.Id);

            if (ingredient == null)
                return (new IngredientResponseDTO(), "Ingrediente no encontrado");
            return (new IngredientResponseDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            }, "OK");
        }
    }
}
