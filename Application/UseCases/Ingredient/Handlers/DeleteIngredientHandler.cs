using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Commands;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Handlers
{
    public class DeleteIngredientHandler : IDeleteIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public DeleteIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<string> Handle(DeleteIngredientCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");
            var ingredient = await _IngredientRepository.GetByIdAsync(command.Id);
            if (ingredient == null)
                throw new NotFoundException("El ingrediente no existe");
            await _IngredientRepository.DeleteAsync(ingredient);
            return "OK";
        }
    }
}
