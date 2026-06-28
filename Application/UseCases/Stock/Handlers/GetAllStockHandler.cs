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

        public async Task<Application.DTOs.PagedResponseDTO<StockResponseDTO>> Handle(GetAllStockQuery query)
        {
            var stocks = await _stockRepository.GetAllAsync(
                query.Page,
                query.PageSize,
                query.OnlyDrinks);
            var totalItems = await _stockRepository.CountAsync(query.OnlyDrinks);

            var stockDtos = stocks.Select(stockEntity => new StockResponseDTO
            {
                Id = stockEntity.Id,
                Count = stockEntity.Count,
                Id_Drink = stockEntity.Id_Drink
            }).ToList();

            return new Application.DTOs.PagedResponseDTO<StockResponseDTO>
            {
                Items = stockDtos,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize)
            };
        }
    }
}
