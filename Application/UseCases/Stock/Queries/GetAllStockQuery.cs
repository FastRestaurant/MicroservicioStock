using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Queries
{
    public class GetAllStockQuery
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetAllStockQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
