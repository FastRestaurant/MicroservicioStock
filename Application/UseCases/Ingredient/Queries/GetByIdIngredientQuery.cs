using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Queries
{
    public class GetByIdIngredientQuery
    {
        public Guid Id { get; }
        public GetByIdIngredientQuery(Guid id)
        {
            Id = id;
        }
    }
}
