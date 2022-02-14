using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrderData> GetOrders();
        ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrderData order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}
