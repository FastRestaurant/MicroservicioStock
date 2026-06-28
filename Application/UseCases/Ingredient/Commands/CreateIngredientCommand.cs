using Domain.Constants;

namespace Application.UseCases.Ingredient.Commands
{
    public class CreateIngredientCommand
    {
        public string Name { get; }
        public decimal InitialStock { get; }
        public UnitType UnitType { get; }

        public CreateIngredientCommand(string name, decimal initialStock, UnitType unitType)
        {
            Name = name;
            InitialStock = initialStock;
            UnitType = unitType;
        }
    }
}
