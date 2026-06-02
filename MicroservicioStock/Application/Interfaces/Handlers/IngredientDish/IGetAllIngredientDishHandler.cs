using Application.DTOs.IngredientDishDTO;
using Application.DTOs.IngredientsDTO;
using Application.UseCases.Ingredient.Queries;
using Application.UseCases.IngredientDish.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.IngredientDish
{
    public interface IGetAllIngredientDishHandler
    {
        Task<(List<IngredientDishResponseDTO> ingredientDishList, string message)> Handle(GetAllIngredientDishQuery query);
    }
}
