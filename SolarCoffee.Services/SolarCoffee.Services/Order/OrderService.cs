using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<IOrderService> _logger;
        private readonly SolarDbContext _db;
        private readonly InventoryService _inventoryService;
        private readonly ProductService _productService;
        public OrderService(ILogger<IOrderService> logger, InventoryService inventoryService, ProductService productService, SolarDbContext db)
        {
            _logger = logger;
            _db = db;
            _inventoryService = inventoryService;
            _productService = productService;
        }
        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            foreach(var item in order.SalesOrderItem)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId,-item.Quantity);

            }
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    Data =  true,
                    IsSuccess = true,
                    Message = "Open Oreders were successfully added!"

                };
            }
            catch (Exception ex) {
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    Data = false,
                    IsSuccess = true,
                    Message =ex.StackTrace
   

            };
            }
           
        }
        /// <summary>
        /// Gets all SalesOrders in the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Cutomer)
                    .ThenInclude(customer =>customer.PrimaryAddress)
                .Include(so => so.SalesOrderItem)
                    .ThenInclude(item =>item.Product)
                .ToList();
        }
/// <summary>
/// Marks an open order salesorder as paid
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = now;
            order.IsPaid = true;
            try 
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    Data = true,
                    IsSuccess = true,
                    Message = $"Order {order.Id} Closed:Order Paid in full!"

                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    Data = false,
                    IsSuccess = false,
                    Message = e.StackTrace

                };
            }


        }
    }
}
