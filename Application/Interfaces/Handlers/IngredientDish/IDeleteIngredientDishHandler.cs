using Application.UseCases.Ingredient.Commands;
using Application.UseCases.IngredientDish.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.IngredientDish
{
    public interface IDeleteIngredientDishHandler
    {
        Task<string> Handle(DeleteIngredientDishCommand command);
    }
}
