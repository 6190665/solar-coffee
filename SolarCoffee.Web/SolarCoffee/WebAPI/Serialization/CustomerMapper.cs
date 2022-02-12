using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.Serialization
{
    public static class CustomerMapper
    {
        /// <summary>
        /// Maps a Customer data model to a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerModel SerializeCustomerModel(Data.Models.Customer customer) {

           return(new CustomerModel
           {
                Id =customer.Id,
               CreatedOn = customer.CreatedOn,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               UpdatedOn=customer.UpdatedOn,
               //PrimaryAddress =customer.PrimaryAddress,
              
           });
          
        }
        /// <summary>
        /// Maps a CustomerModel view model to a Customer data model 
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomerModel(Data.Models.CustomerModel customerModel)
        {

            return (new Customer
            {
                Id = customerModel.Id,
                CreatedOn = customerModel.CreatedOn,
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                UpdatedOn = customerModel.UpdatedOn,
                //PrimaryAddress = customerModel.PrimaryAddress,

            });

        }
    }
}
