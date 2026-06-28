using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Queries
{
    public class GetAllStockQuery
    {
        public int Page { get; }
        public int PageSize { get; }
        public bool OnlyDrinks { get; }

        public GetAllStockQuery(int page = 1, int pageSize = 10, bool onlyDrinks = false)
        {
            Page = Math.Max(1, page);
            PageSize = Math.Clamp(pageSize, 1, 100);
            OnlyDrinks = onlyDrinks;
        }
    }
}
