using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService:ICustomerService
    {
        private readonly SolarDbContext _db;
        public CustomerService(SolarDbContext solarDbContext)
        {
            _db = solarDbContext;
        }
        /// <summary>
        /// Returns a list of customers from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        List<Data.Models.CustomerData> ICustomerService.GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer =>customer.LastName)
                .ToList();
            
        }
        /// <summary>
        /// Gets a customer record
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>ServiceResponse<bool></returns>

        Data.Models.CustomerData ICustomerService.GetCustomerById(int id)
        {
            var cus=_db.Customers.Where(p => p.Id == id).Include(p=>p.PrimaryAddress).FirstOrDefault();
            return cus;
        }

        ServiceResponse<Data.Models.CustomerData> ICustomerService.CreateCustomer(Data.Models.CustomerData customer)
        {
            try
            {
                _db.CustomersAddresses.Add(customer.PrimaryAddress);
                _db.Customers.Add(customer);
                //_db.ProductInventories.Add(new Data.Models.ProductInventory{
                //    customer = customer,
                //    QuantityOnHand =0,
                //    IdealQuantity =10

                //});
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.CustomerData>
                {
                    Data = customer,
                    Message = "New Customer was added successfully!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.CustomerData>
                {
                    Data = customer,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
               
            }
            
          
        }
        /// <summary>
        /// 
        /// Deletes a customer record
        /// </summary>
        /// <param name="id">int customer primary Key</param>
        /// <returns>ServiceResponse<bool></returns>
        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            if(customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message ="Customer to delete not found!",
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
            try {
                _db.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Customer was successfully deleted!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch(Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
           

        }

        
    }
}
