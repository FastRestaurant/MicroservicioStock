using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Commands;
using Domain.Constants;
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
            if (string.IsNullOrWhiteSpace(command.RowVersion))
                throw new ValidationException("La version del ingrediente es obligatoria");

            byte[] rowVersion;
            try
            {
                rowVersion = Convert.FromBase64String(command.RowVersion);
            }
            catch (FormatException)
            {
                throw new ValidationException("La version del ingrediente es invalida");
            }

            var existingIngredient = await _IngredientRepository.GetByIdAsync(id);
            if (existingIngredient == null)
                throw new NotFoundException("Ingrediente no encontrado");

            if (command.UnitType == UnitType.Unit && existingIngredient.Stock?.Count != Math.Truncate(existingIngredient.Stock?.Count ?? 0))
                throw new ValidationException("Los ingredientes por unidad no admiten cantidades decimales");
            
            existingIngredient.Name = command.Name;
            existingIngredient.UnitType = command.UnitType;

            await _IngredientRepository.UpdateAsync(existingIngredient, rowVersion);
            return "OK";


        }
    }
}
