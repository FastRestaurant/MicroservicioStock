using Application.DTOs.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Handlers
{
    public class GetByIdStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public GetByIdStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<(StockResponseDTO stock, string message)> Handle(GetByIdStockQuery query)
        {
            if (query == null)
                return (new StockResponseDTO(), "Consulta inválida");

            if (query.Id == Guid.Empty)
                return (new StockResponseDTO(), "ID de stock inválido");

            var stock = await _stockRepository.GetByIdAsync(query.Id);

            if (stock == null)
                return (new StockResponseDTO(), "Stock no encontrado");

            return (new StockResponseDTO
            {
                Count = stock.Count,
                Id_Drink = stock.Id_Drink

            }, "OK");
        }
    }
}
