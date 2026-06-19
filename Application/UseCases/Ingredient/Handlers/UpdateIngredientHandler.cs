using Application.DTOs.IngredientsDTO;
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
    public class UpdateIngredientHandler : IUpdateIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public UpdateIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<string> Handle(Guid id, UpdateIngredientCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");
            if (string.IsNullOrEmpty(command.Name))
                throw new ValidationException("El nombre del ingrediente es requerido");

            var existingIngredient = await _IngredientRepository.GetByIdAsync(id);
            if (existingIngredient == null)
                throw new NotFoundException("Ingrediente no encontrado");
            
            existingIngredient.Name = command.Name;

            await _IngredientRepository.UpdateAsync(existingIngredient);
            return "OK";


        }
    }
}
