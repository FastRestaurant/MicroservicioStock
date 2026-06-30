using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Commands;
using Domain.Constants;
using Domain.Exceptions;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class ReplaceDishIngredientsHandler : IReplaceDishIngredientsHandler
    {
        private readonly IIngredientDishRepository _ingredientDishRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public ReplaceDishIngredientsHandler(IIngredientDishRepository ingredientDishRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientDishRepository = ingredientDishRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<string> Handle(ReplaceDishIngredientsCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");

            if (command.DishId == Guid.Empty)
                throw new ValidationException("El id del plato es obligatorio");

            if (command.Items == null || command.Items.Count == 0)
                throw new ValidationException("El plato debe tener al menos un ingrediente");

            var duplicatedIngredient = command.Items
                .GroupBy(item => item.Id_Ingredient)
                .FirstOrDefault(group => group.Key != Guid.Empty && group.Count() > 1);

            if (duplicatedIngredient is not null)
                throw new ConflictException("No se puede repetir el mismo ingrediente en el plato");

            foreach (var item in command.Items)
            {
                if (item.Id_Ingredient == Guid.Empty)
                    throw new ValidationException("El ingrediente es obligatorio");

                if (item.RequiredQuantity <= 0)
                    throw new ValidationException("La cantidad requerida debe ser mayor a cero");
            }

            var ingredientIds = command.Items.Select(item => item.Id_Ingredient).ToArray();
            var ingredientsById = (await _ingredientRepository.GetByIdsAsync(ingredientIds))
                .ToDictionary(ingredient => ingredient.Id);

            var recipeItems = new List<Domain.Entities.IngredientDish>();

            foreach (var item in command.Items)
            {
                if (!ingredientsById.TryGetValue(item.Id_Ingredient, out var ingredient))
                    throw new NotFoundException("El ingrediente no existe");

                if (ingredient.UnitType == UnitType.Unit && item.RequiredQuantity != Math.Truncate(item.RequiredQuantity))
                    throw new ValidationException("Los ingredientes por unidad deben tener cantidades enteras");

                recipeItems.Add(new Domain.Entities.IngredientDish
                {
                    Id_Dish = command.DishId,
                    Id_Ingredient = item.Id_Ingredient,
                    RequiredQuantity = item.RequiredQuantity
                });
            }

            await _ingredientDishRepository.ReplaceByDishIdAsync(command.DishId, recipeItems);
            return "OK";
        }
    }
}
