using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;

namespace Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public UnitType UnitType { get; set; } = UnitType.Unit;

        public Guid Id_Stock { get; set; }

        public Stock Stock { get; set; } = null!;

        public List<IngredientDish> IngredientDishes { get; set; } = new();
    }
}
