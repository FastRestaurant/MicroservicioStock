using Application.DTOs.Stock;
using Application.UseCases.Stock.Commands;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IConsumeStockForOrderHandler
    {
        Task<StockOperationResultDTO> Handle(ConsumeStockForOrderCommand command, CancellationToken cancellationToken = default);
    }
}
