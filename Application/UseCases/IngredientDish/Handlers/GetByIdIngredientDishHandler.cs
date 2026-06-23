using Application.DTOs.IngredientDishDTO;
using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Queries;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class GetByIdIngredientDishHandler : IGetByIdIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;

        public GetByIdIngredientDishHandler(IIngredientDishRepository IngredientDishRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
        }

        public async Task<IngredientDishResponseDTO> Handle(GetByIdIngredientDishQuery query)
        {
            if (query == null)
                throw new ValidationException("Datos inválidos");
            if (query.Id == Guid.Empty)
                throw new ValidationException("Id inválido");


            var ingredientDish = await _IngredientDishRepository.GetByIdAsync(query.Id);
            if (ingredientDish == null)
                throw new NotFoundException("El ingrediente del plato no existe");

            return new IngredientDishResponseDTO
            {
                IdIngredientDish = ingredientDish.IdIngredientDish,
                Id_Ingredient = ingredientDish.Id_Ingredient,
                Id_Dish = ingredientDish.Id_Dish,
                RequiredQuantity = ingredientDish.RequiredQuantity
            };
        }
    }
}
