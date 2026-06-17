using Application.DTOs.Stock;
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
    public class UpdateStockHandler : IUpdateStockHandler
    {
        private readonly IStockRepository _stockRepository;

        public UpdateStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<string> Handle(Guid id, UpdateStockCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");

            if (command.Count < 0)
                throw new ValidationException("Cantidad inválida");

            var existing = await _stockRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException("Stock no encontrado");

            existing.Count = command.Count;

            await _stockRepository.UpdateAsync(existing);

            return "Stock actualizado correctamente";
        }
    }
}
