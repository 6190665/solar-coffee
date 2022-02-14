using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Customer;
using SolarCoffee.WebAPI.Serialization;
using SolarCoffee.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolarCoffee.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService =customerService;
        }
        // GET: api/<CustomerController>
        [HttpGet("customers",Name ="GetAllCustomers")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type=typeof(IEnumerable<CustomerModel>))]
        public IActionResult  GetAllCustomers()
        {
            _logger.LogInformation("Getting All Customers");

            var customers = _customerService.GetAllCustomers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customerViews = customers.Select(customer=>CustomerMapper.SerializeCustomerModel(customer));
           
            return Ok(customerViews);
        }
        // GET: api/<CustomerController>
        [HttpGet("{id}", Name = "GeCustomerById")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CustomerModel))]
        public IActionResult GetCustomerById(int id)
        {
            _logger.LogInformation("Getting  Customer by id");

            var customer = _customerService.GetCustomerById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (customer == null)
                return NotFound("Customer Not Found");
            var customerViews = CustomerMapper.SerializeCustomerModel(customer);

            return Ok(customerViews);
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [HttpPost("/api/customer")]
        public ActionResult createCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Creating a new Customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomerModel(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
           
            return Ok(newCustomer);
        }
        [HttpDelete("/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);

        }

    }
}
