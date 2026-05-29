using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IngredientDish
    {
        public Guid Id_Ingredient { get; set;  } 
        public Guid Id_Dish { get; set; }

        public Ingredient Ingredient{ get; set; }

        //public Dish Dish { get; set; }
    }
}
