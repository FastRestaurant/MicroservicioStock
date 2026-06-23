using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Stock
{
    public class StockResponseDTO
    {
        public Guid Id { get; set; }

        public int Count { get; set; }

        public Guid? Id_Drink { get; set; }
    }
}
