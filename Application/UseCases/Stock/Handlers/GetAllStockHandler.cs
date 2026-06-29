using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Queries;
using Domain.Exceptions;

namespace Application.UseCases.Stock.Handlers
{
    public class GetAllStockHandler : IGetAllStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public GetAllStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<StockResponseDTO>> Handle(GetAllStockQuery query)
        {
            var stocks = await _stockRepository.GetAllAsync();

            var stockDtos = stocks.Select(stockEntity => new StockResponseDTO
            {
                Id = stockEntity.Id,
                Count = stockEntity.Count,
                RowVersion = Convert.ToBase64String(stockEntity.RowVersion),
                Id_Drink = stockEntity.Id_Drink
            }).ToList();

            return stockDtos;
        }
    }
}
