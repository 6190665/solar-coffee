using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.ViewModels
{
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
       
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}
