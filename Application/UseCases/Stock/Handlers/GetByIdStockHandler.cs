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
    public class GetByIdStockHandler : IGetByIdStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public GetByIdStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<(StockResponseDTO stock, string message)> Handle(GetByIdStockQuery query)
        {
            if (query == null)
                throw new ValidationException("Consulta inválida");

            if (query.Id == Guid.Empty)
                throw new ValidationException("ID de stock inválido");

            var stock = await _stockRepository.GetByIdAsync(query.Id);

            if (stock == null)
                throw new NotFoundException("Stock no encontrado");

            return (new StockResponseDTO
            {
                Id = stock.Id,
                Count = stock.Count,
                Id_Drink = stock.Id_Drink

            }, "OK");
        }
    }
}
