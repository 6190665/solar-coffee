using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.WebAPI.Serialization;
using SolarCoffee.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }
        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] SalesOrderModel invoice)
        {
            _logger.LogInformation("Generating invoice");
            var saleOrder = SalesOrderMapper.SerializeOrderModel(invoice);
            saleOrder.Cutomer = _customerService.GetCustomerById(invoice.CutomerId);
            var createdInvoice = _orderService.GenerateInvoiceForOrder(saleOrder);
            if (!ModelState.IsValid)
                return BadRequest(createdInvoice);
            return Ok(createdInvoice);

        }
        [HttpGet("/api/order")]
        public ActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            var orderModels = SalesOrderMapper.SerializeOrderModelToViewModels(orders);
            return Ok(orderModels);

        }
        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            _logger.LogInformation($"Marking order {id} complete...");
            var response = _orderService.MarkFulfilled(id);
         
            return Ok(response);

        }
    }
}
