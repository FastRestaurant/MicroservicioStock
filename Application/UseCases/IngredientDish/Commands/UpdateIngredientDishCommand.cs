using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Commands
{
    public class UpdateIngredientDishCommand
    {
        public decimal RequiredQuantity { get; }

        public UpdateIngredientDishCommand(decimal requiredQuantity)
        {
            RequiredQuantity = requiredQuantity;
        }
    }
}
