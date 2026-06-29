namespace Application.UseCases.IngredientDish.Queries
{
    public class GetIngredientDishesByDishQuery
    {
        public Guid DishId { get; }

        public GetIngredientDishesByDishQuery(Guid dishId)
        {
            DishId = dishId;
        }
    }
}
