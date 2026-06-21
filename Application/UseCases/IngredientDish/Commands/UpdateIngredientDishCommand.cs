using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Commands
{
    public class UpdateIngredientDishCommand
    {
        public int RequiredQuantity { get; }

        public UpdateIngredientDishCommand(int requiredQuantity)
        {
            RequiredQuantity = requiredQuantity;
        }
    }
}
