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
    public class CreateStockHandler : ICreateStockHandler
    {
        private readonly IStockRepository _stockRepository;
        //private readonly IDrinkRepository _drinkRepository;
        public CreateStockHandler(IStockRepository stockRepository/*, IDrinkRepository drinkRepository*/)
        {   
            _stockRepository = stockRepository;
            //_drinkRepository = drinkRepository;
        }

        public async Task<string> Handle(CreateStockCommand command)
        {
            if (command == null)
                return "Datos inválidos";

            if (command.Count <= 0)
                return "Cantidad inválida";

            //if(command.Id_Drink == null)
            //    return "ID de bebida inválido";
            //var drinkExists = await _drinkRepository.GetByIdAsync(command.Id_Drink);

            //if (drinkExists==null)
            //    return "Bebida no encontrada";

            var stock = new Domain.Entities.Stock
            {
                Count = command.Count,
                Id_Drink = command.Id_Drink
            };

            await _stockRepository.AddAsync(stock);
            return "OK";
        }
    }
}
