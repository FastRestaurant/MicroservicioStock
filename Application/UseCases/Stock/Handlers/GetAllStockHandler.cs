using Application.DTOs;
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

        public async Task<PagedResponseDTO<StockResponseDTO>> Handle(GetAllStockQuery query)
        {
            var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
            var pageSize = query.PageSize < 1 ? 10 : query.PageSize;
            if (pageSize > 100) pageSize = 100;

            var (stocks, totalCount, currentPage) = await _stockRepository.GetPageAsync(pageNumber, pageSize);

            var stockDtos = stocks.Select(stockEntity => new StockResponseDTO
            {
                Id = stockEntity.Id,
                Count = stockEntity.Count,
                RowVersion = Convert.ToBase64String(stockEntity.RowVersion),
                Id_Drink = stockEntity.Id_Drink
            }).ToList();

            return new PagedResponseDTO<StockResponseDTO>
            {
                Items = stockDtos,
                Page = currentPage,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
