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
    public class CreateIngredientDishHandler : ICreateIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;
        private readonly IIngredientRepository _IngredientRepository;
        //private readonly IDishRepository _DishRepository;

        public CreateIngredientDishHandler(IIngredientDishRepository IngredientDishRepository, IIngredientRepository IngredientRepository/*, IDishRepository DishRepository*/)
        {
            _IngredientDishRepository = IngredientDishRepository;
            _IngredientRepository = IngredientRepository;
            // _DishRepository = DishRepository;
        }

        public async Task<string> Handle(CreateIngredientDishCommand command)
        {
            if (command == null)
                return "Datos invalidos";
            if(command.Id_Dish == Guid.Empty || command.Id_Ingredient== Guid.Empty)
                return "Los IDs son requeridos";


            var ingredientExists = await _IngredientRepository.GetByIdAsync(command.Id_Ingredient);
            if (ingredientExists == null)
                return "El ingrediente no existe";

            //var dishExists = await _DishRepository.GetByIdAsync(command.Id_Dish);
            //if (dishExists == null)
            //    return "El plato no existe";
            
            var existingIngredientDish = await _IngredientDishRepository.GetAllAsync();
            foreach (var ingredientDish2 in existingIngredientDish)
            {
                if (ingredientDish2.Id_Ingredient == command.Id_Ingredient && ingredientDish2.Id_Dish == command.Id_Dish)
                    return "El ingrediente ya está asociado al plato";
            }

            var ingredientDish = new Domain.Entities.IngredientDish
            {
                Id_Ingredient = command.Id_Ingredient,
                Id_Dish = command.Id_Dish
            };

            await _IngredientDishRepository.AddAsync(ingredientDish);
            return "OK";
        }
    }
}
