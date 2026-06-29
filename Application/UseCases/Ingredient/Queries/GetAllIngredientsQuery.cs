using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Ingredient.Queries
{
    public class GetAllIngredientsQuery
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? Search { get; }

        public GetAllIngredientsQuery(int pageNumber, int pageSize, string? search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
        }
    }
}
