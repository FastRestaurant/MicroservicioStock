using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.IngredientDish.Queries
{
    public class GetByIdIngredientDishQuery
    {
        public Guid Id { get; }
        public GetByIdIngredientDishQuery(Guid id)
        {
            Id = id;
        }
    }
}
