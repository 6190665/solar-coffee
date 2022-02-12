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
        List<Data.Models.Customer> GetAllCustomers();
        Data.Models.Customer GetCustomerById(int id);
        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        //ServiceResponse<Data.Models.Customer> ArchiveProduct(int id);
    }
}
