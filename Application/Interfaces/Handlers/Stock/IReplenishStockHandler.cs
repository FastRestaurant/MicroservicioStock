using Application.UseCases.Stock.Commands;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Stock
{
    public interface IReplenishStockHandler
    {
        Task<string> Handle(ReplenishStockCommand command);
    }
}