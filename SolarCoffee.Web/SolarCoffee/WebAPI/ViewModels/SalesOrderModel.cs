using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Data.Models
{
    public class SalesOrderModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerModel Cutomer { get; set; }
        public bool IsPaid { get; set; }
        public List<SalesOrderItem> SalesOrderItem { get; set; }

    }
}
