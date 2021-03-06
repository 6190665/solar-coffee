using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Data.Models
{
    public class SalesOrderData
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerData Cutomer { get; set; }
        public bool IsPaid { get; set; }
        public List<SalesOrderItemData> SalesOrderItems { get; set; }

    }
}
