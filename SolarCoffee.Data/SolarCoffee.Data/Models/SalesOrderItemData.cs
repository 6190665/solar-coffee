using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Data.Models
{
    public class SalesOrderItemData
    {
        public int Id { get; set; }
       
        public int Quantity { get; set; }
        public ProductData Product { get; set; }
    }
}
