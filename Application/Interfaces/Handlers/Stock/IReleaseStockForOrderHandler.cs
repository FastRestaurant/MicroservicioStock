using Application.DTOs.Stock;
using Application.UseCases.Stock.Commands;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IReleaseStockForOrderHandler
    {
        Task<StockOperationResultDTO> Handle(ReleaseStockForOrderCommand command, CancellationToken cancellationToken = default);
    }
}
