using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Commands;
using Application.UseCases.IngredientDish.Commands;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class UpdateIngredientDishHandler : IUpdateIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;
        public UpdateIngredientDishHandler(IIngredientDishRepository IngredientDishRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
        }
        
        

        public async Task<string> Handle(Guid id, UpdateIngredientDishCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");
            if (command.RequiredQuantity <= 0)
                throw new ValidationException("La cantidad requerida es obligatoria");
            
            var existingIngredientDish = await _IngredientDishRepository.GetByIdAsync(id);
            if (existingIngredientDish == null)
                throw new NotFoundException("Ingrediente del plato no encontrado");


            existingIngredientDish.RequiredQuantity = command.RequiredQuantity;

            await _IngredientDishRepository.UpdateAsync(existingIngredientDish);
            
            return "OK";
            
        }
    }
}
