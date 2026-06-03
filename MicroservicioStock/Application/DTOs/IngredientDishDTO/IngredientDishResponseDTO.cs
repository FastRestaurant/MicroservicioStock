using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IngredientDishDTO
{
    public class IngredientDishResponseDTO
    {
        public Guid IdIngredientDish { get; set; }
        public Guid Id_Ingredient { get; set; }
        public Guid Id_Dish { get; set; }
    }
}
