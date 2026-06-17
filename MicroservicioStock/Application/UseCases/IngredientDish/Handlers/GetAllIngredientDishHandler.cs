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
    public class GetAllIngredientDishHandler : IGetAllIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;

        public GetAllIngredientDishHandler(IIngredientDishRepository IngredientDishRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
        }

        public async Task<(List<IngredientDishResponseDTO> ingredientDishList, string message)> Handle(GetAllIngredientDishQuery query)
        {
            var ingredientDishes = await _IngredientDishRepository.GetAllAsync();
            if (ingredientDishes == null || ingredientDishes.Count == 0)
                throw new NotFoundException("No hay ingredientes en los platos");

            var ingredientDishesDTO = ingredientDishes.Select(ingredientDishEntity => new IngredientDishResponseDTO
            {
                IdIngredientDish = ingredientDishEntity.IdIngredientDish,
                Id_Ingredient = ingredientDishEntity.Id_Ingredient,
                Id_Dish = ingredientDishEntity.Id_Dish,
                RequiredQuantity = ingredientDishEntity.RequiredQuantity
            }).ToList();
            return (ingredientDishesDTO, "OK");
        }
    }
}
