using Application.Interfaces.Repositories;
using Application.Interfaces.Handlers.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Ingredient.Commands;

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
                return "Datos invalidos";

            if(string.IsNullOrEmpty(command.Name))
                return "El nombre del ingrediente es requerido";
            
            var ingredientexists = await _IngredientRepository.GetByNameAsync(command.Name);
            if (ingredientexists != null)
                return "El ingrediente ya existe";

            var ingredient = new Domain.Entities.Ingredient
            {
                Name = command.Name
            };
            await _IngredientRepository.AddAsync(ingredient);
            return "OK";
        }
    }
}
