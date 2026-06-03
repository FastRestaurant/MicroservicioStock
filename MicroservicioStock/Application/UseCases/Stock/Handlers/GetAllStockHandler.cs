using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Queries;

namespace Application.UseCases.Stock.Handlers
{
    public class GetAllStockHandler : IGetAllStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public GetAllStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<(List<StockResponseDTO> Stocks, string message)> Handle(GetAllStockQuery query)
        {
            var stocks = await _stockRepository.GetAllAsync();

            if (stocks == null || !stocks.Any())
                return (new List<StockResponseDTO>(), "No hay productos en stock");

            var stockDtos = stocks.Select(stockEntity => new StockResponseDTO
            {
                Count = stockEntity.Count,
                Id_Drink = stockEntity.Id_Drink
            }).ToList();

            return (stockDtos, "OK");
        }
    }
}
