using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Commands
{
    public class UpdateStockCommand
    {
        public int Count { get; }

        public UpdateStockCommand(int count)
        {
            Count = count;
            
        }
    }
}
