using Application.UseCases.Ingredient.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Ingredient
{
    public interface ICreateIngredientHandler
    {
        Task<string> Handle(CreateIngredientCommand command);
    }
}
