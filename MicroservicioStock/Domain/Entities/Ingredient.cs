using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid Id_Stock { get; set; }

        public Stock Stock { get; set; }

        public List<IngredientDish> IngredientDishes { get; set; }
    }
}
