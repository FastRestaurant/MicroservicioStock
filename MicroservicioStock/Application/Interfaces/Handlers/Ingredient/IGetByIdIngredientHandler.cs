using Application.DTOs.IngredientsDTO;
using Application.UseCases.Ingredient.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Ingredient
{
    public interface IGetByIdIngredientHandler
    {
        Task<(IngredientResponseDTO ingredient, string message)> Handle(GetByIdIngredientQuery query);
    }
}
