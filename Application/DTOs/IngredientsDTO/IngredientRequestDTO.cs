using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IngredientsDTO
{
    public class IngredientRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public int InitialStock { get; set; }
    }
}
