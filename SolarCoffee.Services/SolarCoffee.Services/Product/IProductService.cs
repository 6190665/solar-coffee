using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Data.Models.ProductData> GetAllProducts();
        Data.Models.ProductData GetProductById(int id);
        ServiceResponse<Data.Models.ProductData> CreateProduct(Data.Models.ProductData product);
        ServiceResponse<Data.Models.ProductData> ArchiveProduct(int id);
    }
}
