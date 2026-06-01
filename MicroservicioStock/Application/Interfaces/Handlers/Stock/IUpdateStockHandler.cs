using Application.DTOs.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IUpdateStockHandler
    {
        Task<string> Handle(Guid id, StockRequestDTO dto);
    }
}
