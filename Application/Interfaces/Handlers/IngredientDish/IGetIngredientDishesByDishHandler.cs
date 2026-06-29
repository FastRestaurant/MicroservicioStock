using Application.DTOs.IngredientDishDTO;
using Application.UseCases.IngredientDish.Queries;

namespace Application.Interfaces.Handlers.IngredientDish
{
    public interface IGetIngredientDishesByDishHandler
    {
        Task<List<IngredientDishResponseDTO>> Handle(GetIngredientDishesByDishQuery query);
    }
}
