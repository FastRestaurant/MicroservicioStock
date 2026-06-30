using Application.DTOs.Stock;
using Application.DTOs;
using Application.UseCases.Stock.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IGetAllStockHandler
    {
        Task<PagedResponseDTO<StockResponseDTO>> Handle(GetAllStockQuery query);
    }
}
