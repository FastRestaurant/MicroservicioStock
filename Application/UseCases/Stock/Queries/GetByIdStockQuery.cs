using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Queries
{
    public class GetByIdStockQuery
    {
        public Guid Id { get; } 
        public GetByIdStockQuery (Guid id)
        {
            Id = id;
        }
    }
}
