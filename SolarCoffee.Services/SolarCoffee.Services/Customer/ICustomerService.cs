using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Data;

namespace SolarCoffee.Services.Customer

{
    public interface ICustomerService
    {
        List<Data.Models.CustomerData> GetAllCustomers();
        Data.Models.CustomerData GetCustomerById(int id);
        ServiceResponse<Data.Models.CustomerData> CreateCustomer(Data.Models.CustomerData customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        //ServiceResponse<Data.Models.Customer> ArchiveProduct(int id);
    }
}
