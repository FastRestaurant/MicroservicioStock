using Application.UseCases.IngredientDish.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.IngredientDish
{
    public interface IUpdateIngredientDishHandler
    {
        Task<string> Handle(Guid id, UpdateIngredientDishCommand command);
    }
}
