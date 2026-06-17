using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.UseCases.IngredientDish.Commands
{
    public class CreateIngredientDishCommand
    {
        public Guid Id_Ingredient { get; }
        public Guid Id_Dish { get; }
        public int RequiredQuantity { get; }

        public CreateIngredientDishCommand(Guid id_ingredient, Guid id_dish, int requiredQuantity)
        {
            Id_Ingredient = id_ingredient;
            Id_Dish = id_dish;
            RequiredQuantity = requiredQuantity;
        }
    }
}
