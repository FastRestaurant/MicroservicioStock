using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using Domain.Exceptions;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Handlers
{
    public class ReplenishStockHandler : IReplenishStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public ReplenishStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<string> Handle(ReplenishStockCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");

            if (command.Quantity <= 0)
                throw new ValidationException("La cantidad a reponer debe ser mayor a cero");

            // 1. Obtener el stock actual
            var stock = await _stockRepository.GetByIdAsync(command.StockId);
            if (stock == null)
                throw new NotFoundException("Stock no encontrado");

            // 2. Sumar la cantidad ingresada (Reposición)
            stock.Count += command.Quantity;

            // 3. Persistir cambios
            await _stockRepository.UpdateAsync(stock);

            return "Stock repuesto correctamente";
        }
    }
}