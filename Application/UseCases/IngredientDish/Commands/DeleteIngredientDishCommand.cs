using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Commands
{
    public class DeleteIngredientDishCommand
    {
        public Guid Id { get; }

        public DeleteIngredientDishCommand(Guid id)
        {
            Id = id;
        }
    }
}
