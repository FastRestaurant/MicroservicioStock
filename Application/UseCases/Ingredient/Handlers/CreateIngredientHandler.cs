using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Handlers.Ingredient;
using Application.UseCases.Ingredient.Commands;
using Domain.Constants;
using Domain.Exceptions;

namespace Application.UseCases.Ingredient.Handlers
{
    public class CreateIngredientHandler : ICreateIngredientHandler
    {
        private readonly IIngredientRepository _IngredientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIngredientHandler(IIngredientRepository IngredientRepository, IUnitOfWork unitOfWork)
        {
            _IngredientRepository = IngredientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateIngredientCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");

            if (string.IsNullOrEmpty(command.Name))
                throw new ValidationException("El nombre del ingrediente es requerido");

            if (command.InitialStock < 0)
                throw new ValidationException("El stock inicial no puede ser negativo");

            if (command.UnitType == UnitType.Unit && command.InitialStock != Math.Truncate(command.InitialStock))
                throw new ValidationException("Los ingredientes por unidad no admiten cantidades decimales");

            var ingredientexists = await _IngredientRepository.GetByNameAsync(command.Name);
            if (ingredientexists != null)
                throw new ConflictException("El ingrediente ya existe");

            var ingredient = new Domain.Entities.Ingredient
            {
                Name = command.Name,
                UnitType = command.UnitType,
                Stock = new Domain.Entities.Stock
                {
                    Count = command.InitialStock
                }
            };
            await _IngredientRepository.AddAsync(ingredient);
            await _unitOfWork.SaveChangesAsync();
            return "OK";
        }
    }
}
