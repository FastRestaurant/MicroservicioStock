namespace Application.UseCases.Stock.Commands
{
    public class ReleaseStockForOrderCommand
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
