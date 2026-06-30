using Domain.Constants;

namespace Application.DTOs.IngredientsDTO
{
    public class IngredientResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StockId { get; set; }
        public decimal StockCount { get; set; }
        public UnitType UnitType { get; set; } = UnitType.Unit;
        public string RowVersion { get; set; } = string.Empty;
    }
}
