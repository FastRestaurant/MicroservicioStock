using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Commands
{
    public class CreateStockCommand
    {
        public int Count { get;}

        public Guid? Id_Drink { get; }

        public CreateStockCommand(int count, Guid? id_Drink)
        {
            Count = count;
            Id_Drink = id_Drink;
        }
    }
}
