using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IngredientDishDTO
{
    public class IngredientDishRequestDTO
    {
        public Guid Id_Ingredient { get; set; }
        public Guid Id_Dish { get; set; }
        public decimal RequiredQuantity { get; set; }
        public string RowVersion { get; set; } = string.Empty;
    }

    public class DishIngredientRequestDTO
    {
        public Guid Id_Ingredient { get; set; }
        public decimal RequiredQuantity { get; set; }
    }

    public class ReplaceDishIngredientsRequestDTO
    {
        public List<DishIngredientRequestDTO> Items { get; set; } = new();
    }
}
