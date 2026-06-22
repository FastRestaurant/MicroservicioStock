using System;

namespace Application.UseCases.Stock.Commands
{
    public class ReplenishStockCommand
    {
        public Guid StockId { get; }
        public int Quantity { get; }

        public ReplenishStockCommand(Guid stockId, int quantity)
        {
            StockId = stockId;
            Quantity = quantity;
        }
    }
}