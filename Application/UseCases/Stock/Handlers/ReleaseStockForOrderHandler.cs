using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using Domain.Exceptions;

namespace Application.UseCases.Stock.Handlers
{
    public class ReleaseStockForOrderHandler : IReleaseStockForOrderHandler
    {
        private readonly IStockConsumptionRepository _stockConsumptionRepository;

        public ReleaseStockForOrderHandler(IStockConsumptionRepository stockConsumptionRepository)
        {
            _stockConsumptionRepository = stockConsumptionRepository;
        }

        public async Task<StockOperationResultDTO> Handle(ReleaseStockForOrderCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");

            if (command.OrderId == Guid.Empty || command.OrderItemId == Guid.Empty)
                throw new ValidationException("Los IDs de orden e item son obligatorios");

            return await _stockConsumptionRepository.ReleaseForOrderAsync(command, cancellationToken);
        }
    }
}
