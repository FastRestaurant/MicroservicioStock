using Application.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIngredientHandler(IIngredientRepository IngredientRepository, IUnitOfWork unitOfWork)
        {
            _IngredientRepository = IngredientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteIngredientCommand command)
        {
            if (command == null)
                throw new ValidationException("Datos invalidos");
            var ingredient = await _IngredientRepository.GetByIdAsync(command.Id);
            if (ingredient == null)
                throw new NotFoundException("El ingrediente no existe");

            if (await _IngredientRepository.HasAssignedDishesAsync(command.Id))
                throw new ConflictException("No se puede eliminar el ingrediente porque tiene platos asignados.");

            await _IngredientRepository.DeleteAsync(ingredient);
            await _unitOfWork.SaveChangesAsync();
            return "OK";
        }
    }
}
