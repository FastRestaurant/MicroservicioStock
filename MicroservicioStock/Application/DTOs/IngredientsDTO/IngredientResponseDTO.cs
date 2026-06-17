using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IngredientsDTO
{
    public class IngredientResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StockId { get; set; }
        public int StockCount { get; set; }

    }
}
