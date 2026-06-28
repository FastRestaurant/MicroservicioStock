using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Commands;
using Domain.Constants;
using Domain.Exceptions;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class CreateIngredientDishHandler : ICreateIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;
        private readonly IIngredientRepository _IngredientRepository;

        public CreateIngredientDishHandler(IIngredientDishRepository IngredientDishRepository, IIngredientRepository IngredientRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
            _IngredientRepository = IngredientRepository;
        }

        public async Task<string> Handle(CreateIngredientDishCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");
            if (command.Id_Dish == Guid.Empty || command.Id_Ingredient == Guid.Empty)
                throw new ValidationException("Los IDs son requeridos");

            if (command.RequiredQuantity <= 0)
                throw new ValidationException("La cantidad requerida debe ser mayor a cero");

            var ingredient = await _IngredientRepository.GetByIdAsync(command.Id_Ingredient);
            if (ingredient == null)
                throw new NotFoundException("El ingrediente no existe");

            if (ingredient.UnitType == UnitType.Unit && command.RequiredQuantity != Math.Truncate(command.RequiredQuantity))
                throw new ValidationException("El ingrediente se mide por unidad; la cantidad requerida debe ser entera");

            var existingIngredientDish = await _IngredientDishRepository.GetAllAsync();
            foreach (var ingredientDish2 in existingIngredientDish)
            {
                if (ingredientDish2.Id_Ingredient == command.Id_Ingredient && ingredientDish2.Id_Dish == command.Id_Dish)
                    throw new ConflictException("El ingrediente ya está asociado al plato");
            }

            var ingredientDish = new Domain.Entities.IngredientDish
            {
                Id_Ingredient = command.Id_Ingredient,
                Id_Dish = command.Id_Dish,
                RequiredQuantity = command.RequiredQuantity
            };

            await _IngredientDishRepository.AddAsync(ingredientDish);
            return "OK";
        }
    }
}
