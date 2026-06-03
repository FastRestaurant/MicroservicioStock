using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class DeleteIngredientDishHandler : IDeleteIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;

        public DeleteIngredientDishHandler(IIngredientDishRepository IngredientDishRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
        }

        public async Task<string> Handle(DeleteIngredientDishCommand command)
        {
            if(command == null)
                return "Datos invalidos";

            var ingredientDish = await _IngredientDishRepository.GetByIdAsync(command.Id);
            if (ingredientDish == null)
                return "El ingrediente del plato no existe";
            await _IngredientDishRepository.DeleteAsync(ingredientDish);
            return "OK";
        }
    }
}
