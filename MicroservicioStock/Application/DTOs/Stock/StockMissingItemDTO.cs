namespace Application.DTOs.Stock
{
    public class StockMissingItemDTO
    {
        public Guid? IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RequiredQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
