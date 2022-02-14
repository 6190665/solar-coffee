using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.ViewModels
{/// <summary>
/// View Model for open SalesOrders/// </summary>
    public class SalesOrderModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CutomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public bool IsPaid { get; set; }
        public List<SalesOrderItemModel> SalesOrderItems { get; set; }

    }
}
