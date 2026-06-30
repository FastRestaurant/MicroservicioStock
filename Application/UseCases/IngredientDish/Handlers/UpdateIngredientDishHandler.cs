using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Commands;
using Domain.Constants;
using Domain.Exceptions;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class UpdateIngredientDishHandler : IUpdateIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;
        private readonly IIngredientRepository _IngredientRepository;

        public UpdateIngredientDishHandler(IIngredientDishRepository IngredientDishRepository, IIngredientRepository IngredientRepository)
        {
            _IngredientDishRepository = IngredientDishRepository;
            _IngredientRepository = IngredientRepository;
        }

        public async Task<string> Handle(Guid id, UpdateIngredientDishCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos inválidos");
            if (command.RequiredQuantity <= 0)
                throw new ValidationException("La cantidad requerida es obligatoria");
            if (string.IsNullOrWhiteSpace(command.RowVersion))
                throw new ValidationException("La version del ingrediente del plato es obligatoria");

            byte[] rowVersion;
            try
            {
                rowVersion = Convert.FromBase64String(command.RowVersion);
            }
            catch (FormatException)
            {
                throw new ValidationException("La version del ingrediente del plato es invalida");
            }

            var existingIngredientDish = await _IngredientDishRepository.GetByIdAsync(id);
            if (existingIngredientDish == null)
                throw new NotFoundException("Ingrediente del plato no encontrado");

            var ingredient = await _IngredientRepository.GetByIdAsync(existingIngredientDish.Id_Ingredient);
            if (ingredient == null)
                throw new NotFoundException("El ingrediente no existe");

            if (ingredient.UnitType == UnitType.Unit && command.RequiredQuantity != Math.Truncate(command.RequiredQuantity))
                throw new ValidationException("El ingrediente se mide por unidad; la cantidad requerida debe ser entera");

            existingIngredientDish.RequiredQuantity = command.RequiredQuantity;

            await _IngredientDishRepository.UpdateAsync(existingIngredientDish, rowVersion);

            return "OK";
        }
    }
}
