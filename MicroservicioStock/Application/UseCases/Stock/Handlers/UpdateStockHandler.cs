using Application.DTOs.Stock;
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
    public class UpdateStockHandler : IUpdateStockHandler
    {
        private readonly IStockRepository _stockRepository;
        //private readonly IDrinkRepository _drinkRepository;

        public UpdateStockHandler(IStockRepository stockRepository/*, IDrinkRepository drinkRepository*/)
        {
            _stockRepository = stockRepository;
            //_drinkRepository = drinkRepository;
        }
        public async Task<string> Handle(Guid id, UpdateStockCommand command)
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
            var existing = await _stockRepository.GetByIdAsync(id);

            existing.Count = command.Count;

            await _stockRepository.UpdateAsync(existing);

            return "Stock actualizado correctamente";
        }
    }
}
