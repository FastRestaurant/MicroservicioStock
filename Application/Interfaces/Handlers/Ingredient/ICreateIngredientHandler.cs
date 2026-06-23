using Application.UseCases.Ingredient.Commands;

namespace Application.Interfaces.Handlers.Ingredient
{
    public interface ICreateIngredientHandler
    {
        Task<string> Handle(CreateIngredientCommand command);
    }
}
