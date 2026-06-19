using Application.DTOs.Stock;
using Application.UseCases.Stock.Commands;

namespace Application.Interfaces.Repositories
{
    public interface IStockConsumptionRepository
    {
        Task<StockOperationResultDTO> ConsumeForOrderAsync(ConsumeStockForOrderCommand command, CancellationToken cancellationToken = default);
        Task<StockOperationResultDTO> ReleaseForOrderAsync(ReleaseStockForOrderCommand command, CancellationToken cancellationToken = default);
    }
}
