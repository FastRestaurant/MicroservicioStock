using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Constants;

namespace Application.UseCases.Ingredient.Commands
{
    public class UpdateIngredientCommand
    {
        public string Name { get; }
        public UnitType UnitType { get; }

        public UpdateIngredientCommand(string name, UnitType unitType)
        {

            Name = name;
            UnitType = unitType;
        }
    }
}
