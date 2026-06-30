using Application.Interfaces;
using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.IngredientDish.Commands;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Handlers
{
    public class DeleteIngredientDishHandler : IDeleteIngredientDishHandler
    {
        private readonly IIngredientDishRepository _IngredientDishRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIngredientDishHandler(IIngredientDishRepository IngredientDishRepository, IUnitOfWork unitOfWork)
        {
            _IngredientDishRepository = IngredientDishRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteIngredientDishCommand command)
        {
            if(command == null)
                throw new ValidationException("Datos invalidos");

            var ingredientDish = await _IngredientDishRepository.GetByIdAsync(command.Id);
            if (ingredientDish == null)
                throw new NotFoundException("El ingrediente del plato no existe");
            await _IngredientDishRepository.DeleteAsync(ingredientDish);
            await _unitOfWork.SaveChangesAsync();
            return "OK";
        }
    }
}
