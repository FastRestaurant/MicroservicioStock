using Application.UseCases.Ingredient.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Ingredient
{
    public interface IDeleteIngredientHandler
    {
        Task<string> Handle(DeleteIngredientCommand command);
    }
}
