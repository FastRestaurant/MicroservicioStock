using Application.DTOs.IngredientsDTO;
using Application.UseCases.Ingredient.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Ingredient
{
    public interface IGetAllIngredientHandler
    {
        Task<Application.DTOs.PagedResponseDTO<IngredientResponseDTO>> Handle(GetAllIngredientsQuery query);
    }
}
