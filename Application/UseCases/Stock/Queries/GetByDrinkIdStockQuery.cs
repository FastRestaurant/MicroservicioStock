using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Queries
{
    public class GetByDrinkIdStockQuery
    {
        public Guid DrinkId { get; }
        public GetByDrinkIdStockQuery(Guid drinkId)
        {
            DrinkId = drinkId;
        }
    }
}
