using Domain.Constants;

namespace Application.DTOs.IngredientsDTO
{
    public class IngredientRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal InitialStock { get; set; }
        public UnitType UnitType { get; set; } = UnitType.Unit;
        public string RowVersion { get; set; } = string.Empty;
    }
}
