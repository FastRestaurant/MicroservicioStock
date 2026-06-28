using Domain.Constants;

namespace Application.DTOs.Stock
{
    public class StockMissingItemDTO
    {
        public Guid? IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; }
        public decimal AvailableQuantity { get; set; }
        public UnitType? UnitType { get; set; }
    }
}
