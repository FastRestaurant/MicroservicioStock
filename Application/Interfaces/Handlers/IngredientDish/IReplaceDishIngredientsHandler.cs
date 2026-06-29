using Application.UseCases.IngredientDish.Commands;

namespace Application.Interfaces.Handlers.IngredientDish
{
    public interface IReplaceDishIngredientsHandler
    {
        Task<string> Handle(ReplaceDishIngredientsCommand command);
    }
}
