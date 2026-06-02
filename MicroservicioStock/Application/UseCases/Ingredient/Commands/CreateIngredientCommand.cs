using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Commands
{
    public class CreateIngredientCommand
    {
       
        public string Name { get; }

        public CreateIngredientCommand(string name)
        {
            
            Name = name;
        }
    }
}
