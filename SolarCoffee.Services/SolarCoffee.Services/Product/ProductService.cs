using SolarCoffee.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Product
{
    public class ProductService:IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext solarDbContext)
        {
            _db = solarDbContext;
        }

        List<Data.Models.Product> IProductService.GetAllProducts()
        {
            return _db.Products.ToList();
        }

        Data.Models.Product IProductService.GetProductById(int id)
        {
            var pro=_db.Products.Where(p => p.Id == id).FirstOrDefault();
            return pro;
        }

        ServiceResponse<Data.Models.Product> IProductService.CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);
                _db.ProductInventories.Add(new Data.Models.ProductInventory{ 
                    Product = product,
                    QuantityOnHand =0,
                    IdealQuantity =10

                });
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Message = "Product was added successfully!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Message = $"{e}Error Saving changes",
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
               
            }
            
          
        }

        ServiceResponse<Data.Models.Product> IProductService.ArchiveProduct(int id)
        {
            var product =_db.Products.Where(p => p.Id == id).FirstOrDefault();
            try
            {
                product.IsArchived = true;
                _db.Add(product);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Message = "Archived Product!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }

        }

    }
}
