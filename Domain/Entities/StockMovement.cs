namespace Domain.Entities
{
    public class StockMovement
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductType { get; set; } = string.Empty;
        public Guid StockId { get; set; }
        public Guid? IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Stock Stock { get; set; } = null!;
    }
}
