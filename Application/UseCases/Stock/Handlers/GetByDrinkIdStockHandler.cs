using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Queries;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Handlers
{
    public class GetByDrinkIdStockHandler : IGetByDrinkIdStockHandler
    {
        private readonly IStockRepository _stockRepository;
        public GetByDrinkIdStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<StockResponseDTO> Handle(GetByDrinkIdStockQuery query)
        {
            if (query == null)
                throw new ValidationException("Consulta inválida");
            if (query.DrinkId == Guid.Empty)
                throw new ValidationException("ID de bebida inválido");
            var stock = await _stockRepository.GetByDrinkIdAsync(query.DrinkId);
            if (stock == null)
                throw new NotFoundException("Stock no encontrado");
            return new StockResponseDTO
            {
                Id = stock.Id,
                Count = stock.Count,
                Id_Drink = stock.Id_Drink
            };
        }
    }
}
