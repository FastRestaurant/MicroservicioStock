using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Queries
{
    public class GetAllIngredientsQuery
    {
        public int Page { get; }
        public int PageSize { get; }

        public GetAllIngredientsQuery(int page = 1, int pageSize = 10)
        {
            Page = Math.Max(1, page);
            PageSize = Math.Clamp(pageSize, 1, 100);
        }
    }
}
