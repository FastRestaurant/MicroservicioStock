using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using Domain.Constants;
using Domain.Exceptions;

namespace Application.UseCases.Stock.Handlers
{
    public class ConsumeStockForOrderHandler : IConsumeStockForOrderHandler
    {
        private readonly IStockConsumptionRepository _stockConsumptionRepository;

        public ConsumeStockForOrderHandler(IStockConsumptionRepository stockConsumptionRepository)
        {
            _stockConsumptionRepository = stockConsumptionRepository;
        }

        public async Task<StockOperationResultDTO> Handle(ConsumeStockForOrderCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");

            if (command.OrderId == Guid.Empty || command.OrderItemId == Guid.Empty || command.ProductId == Guid.Empty)
                throw new ValidationException("Los IDs de orden, item y producto son obligatorios");

            if (string.IsNullOrWhiteSpace(command.ProductType))
                throw new ValidationException("El tipo de producto es obligatorio");

            if (!ProductTypes.IsValid(command.ProductType))
                throw new ValidationException("El tipo de producto debe ser 'Dish' o 'Drink'.");

            if (command.Quantity <= 0)
                throw new ValidationException("La cantidad debe ser mayor a cero");

            return await _stockConsumptionRepository.ConsumeForOrderAsync(command, cancellationToken);
        }
    }
}
