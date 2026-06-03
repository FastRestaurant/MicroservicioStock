using Application.UseCases.Stock.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IDeleteStockHandler
    {
        Task<string> Handle(DeleteStockCommand command);
    }
}
