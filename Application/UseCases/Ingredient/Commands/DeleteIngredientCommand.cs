using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Commands
{
    public class DeleteIngredientCommand
    {
        public Guid Id { get; }

        public DeleteIngredientCommand(Guid id)
        {
            Id = id;
        }
    }
}
