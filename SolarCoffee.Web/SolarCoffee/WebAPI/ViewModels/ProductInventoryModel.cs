using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.ViewModels
{
    public class ProductInventoryModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int QuantityOnHand  { get; set; }
        public int IdealQuantity  { get; set; }
        public ProductModel Product { get; set; }
    }
}
