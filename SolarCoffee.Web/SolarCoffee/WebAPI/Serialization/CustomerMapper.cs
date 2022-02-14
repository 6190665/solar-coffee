using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data.Models;
using SolarCoffee.WebAPI.ViewModels;
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
        public static CustomerModel SerializeCustomerModel(CustomerData customer) {
          
           return(new CustomerModel
           {
                Id =customer.Id,
               CreatedOn = customer.CreatedOn,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               UpdatedOn=customer.UpdatedOn,
               PrimaryAddress = SerializeCustomerAddress(customer.PrimaryAddress)
              
           });
          
        }
        /// <summary>
        /// Maps a CustomerModel view model to a Customer data model 
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public static CustomerData SerializeCustomerModel(CustomerModel customer)
        {
           

            return (new CustomerData
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UpdatedOn = customer.UpdatedOn,
                PrimaryAddress = SerializeCustomerAddress(customer.PrimaryAddress),

            });

        }
        public static CustomerAddressData SerializeCustomerAddress(CustomerAddressModel customerAddress)
        {
            return new CustomerAddressData
            {
                Id = customerAddress.Id,
                AddressLine1 = customerAddress.AddressLine1,
                AddressLine2 = customerAddress.AddressLine2,
                City = customerAddress.City,
                State = customerAddress.State,
                Country = customerAddress.Country,
                PostalCode = customerAddress.PostalCode,
                CreatedOn = customerAddress.CreatedOn,
                UpdatedOn = customerAddress.UpdatedOn,
            };

        } 
        public static CustomerAddressModel SerializeCustomerAddress(CustomerAddressData customerAddress)
        {
            return new CustomerAddressModel
            {
                Id = customerAddress.Id,
                AddressLine1 = customerAddress.AddressLine1,
                AddressLine2 = customerAddress.AddressLine2,
                City = customerAddress.City,
                State = customerAddress.State,
                Country = customerAddress.Country,
                PostalCode = customerAddress.PostalCode,
                CreatedOn = customerAddress.CreatedOn,
                UpdatedOn = customerAddress.UpdatedOn,
            };

        }
        
    }
}
