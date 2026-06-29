using Application.DTOs.IngredientDishDTO;

namespace Application.UseCases.IngredientDish.Commands
{
    public class ReplaceDishIngredientsCommand
    {
        public Guid DishId { get; }
        public List<DishIngredientRequestDTO> Items { get; }

        public ReplaceDishIngredientsCommand(Guid dishId, List<DishIngredientRequestDTO> items)
        {
            DishId = dishId;
            Items = items;
        }
    }
}
