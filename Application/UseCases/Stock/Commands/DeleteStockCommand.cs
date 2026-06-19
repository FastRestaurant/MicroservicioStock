using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Stock.Commands
{
    public class DeleteStockCommand
    {
        public Guid Id { get; }
        public DeleteStockCommand(Guid id)
        {
            Id = id;
        }
    }
}
