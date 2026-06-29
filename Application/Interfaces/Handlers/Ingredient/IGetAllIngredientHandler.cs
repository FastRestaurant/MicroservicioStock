using Application.DTOs.IngredientsDTO;
using Application.DTOs;
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
        Task<PagedResponseDTO<IngredientResponseDTO>> Handle(GetAllIngredientsQuery query);
    }
}
