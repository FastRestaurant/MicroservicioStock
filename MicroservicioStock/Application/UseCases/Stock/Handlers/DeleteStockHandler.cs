using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Handlers
{
    public class DeleteStockHandler : IDeleteStockHandler
    {
        private readonly IStockRepository _stockRepository;
        public DeleteStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<string> Handle(DeleteStockCommand command)
        {
            
            if (command == null)
                return "Stock no encontrado";

            var stock = await _stockRepository.GetByIdAsync(command.Id);
            if(stock == null)
                return "Stock no encontrado";

            await _stockRepository.DeleteAsync(command.Id);
            return "Stock eliminado correctamente";
        }
    }
}
