using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using Domain.Exceptions;
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
                throw new ValidationException("Datos inválidos");

            var stock = await _stockRepository.GetByIdAsync(command.Id);
            if(stock == null)
                throw new NotFoundException("Stock no encontrado");

            if (await _stockRepository.HasAssignedDishesAsync(command.Id))
                throw new ConflictException("No se puede eliminar el stock porque tiene platos asignados.");

            await _stockRepository.DeleteAsync(command.Id);
            return "Stock eliminado correctamente";
        }
    }
}
