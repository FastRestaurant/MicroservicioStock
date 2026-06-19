using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Commands
{
    public class UpdateIngredientCommand
    {
        public string Name { get; }

        public UpdateIngredientCommand(string name)
        {

            Name = name;
        }
    }
}
