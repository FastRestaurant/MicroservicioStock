namespace Application.UseCases.Stock.Commands
{
    public class ConsumeStockForOrderCommand
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductType { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
