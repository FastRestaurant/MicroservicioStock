using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }
        public int Count { get; set; }

        public Guid Id_Drink { get; set; }  

        public List<Ingredient> Ingredients { get; set; }

        //public Drink Drink { get; set; }
    }
}
