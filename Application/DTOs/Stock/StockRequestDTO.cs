using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Stock
{
    public class StockRequestDTO
    {
        public Guid Id { get; set; }
        public decimal Count { get; set; }

        public string RowVersion { get; set; } = string.Empty;

        public Guid? Id_Drink { get; set; }
    }
}
