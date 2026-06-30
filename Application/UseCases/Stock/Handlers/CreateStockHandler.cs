using Application.Interfaces;
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
    public class CreateStockHandler : ICreateStockHandler
    {
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStockHandler(IStockRepository stockRepository, IUnitOfWork unitOfWork)
        {   
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateStockCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");

            if (command.Count < 0)
                throw new ValidationException("Cantidad inválida");

            var stock = new Domain.Entities.Stock
            {
                Count = command.Count,
                Id_Drink = command.Id_Drink
            };

            await _stockRepository.AddAsync(stock);
            await _unitOfWork.SaveChangesAsync();
            return "Stock creado correctamente";
        }
    }
}
