using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IngredientDish
    {
        public Guid IdIngredientDish { get; set; }
        public Guid Id_Ingredient { get; set;  } 
        public Guid Id_Dish { get; set; }
        public decimal RequiredQuantity { get; set; } = 1;
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public Ingredient Ingredient{ get; set; } = null!;

    }
}
