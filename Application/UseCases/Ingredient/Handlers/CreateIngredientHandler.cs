using Application.Interfaces.Repositories;
using Application.Interfaces.Handlers.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Ingredient.Commands;
using Domain.Exceptions;

namespace Application.UseCases.Ingredient.Handlers
{
    public class CreateIngredientHandler : ICreateIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;

        public CreateIngredientHandler(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<string> Handle(CreateIngredientCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");

            if(string.IsNullOrEmpty(command.Name))
                throw new ValidationException("El nombre del ingrediente es requerido");

            if (command.InitialStock < 0)
                throw new ValidationException("El stock inicial no puede ser negativo");
              
            var ingredientexists = await _IngredientRepository.GetByNameAsync(command.Name);
            if (ingredientexists != null)
                throw new ConflictException("El ingrediente ya existe");

            var ingredient = new Domain.Entities.Ingredient
            {
                Name = command.Name,
                Stock = new Domain.Entities.Stock
                {
                    Count = command.InitialStock
                }
            };
            await _IngredientRepository.AddAsync(ingredient);
            return "OK";
        }
    }
}
