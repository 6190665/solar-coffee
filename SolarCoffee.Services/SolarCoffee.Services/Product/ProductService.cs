using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext solarDbContext)
        {
            _db = solarDbContext;
        }
        public ServiceResponse<ProductData> ArchiveProduct(int id)
        {
            var product = _db.Products.Where(p => p.Id == id).FirstOrDefault();
            try
            {
               
                product.IsArchived = true;
                
                _db.Products.Update(product);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.ProductData>
                {
                    Data = product,
                    Message = "Archived Product!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.ProductData>
                {
                    Data = product,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<Data.Models.ProductData> CreateProduct(Data.Models.ProductData product)
        {
            try
            {
                _db.Products.Add(product);
                _db.ProductInventories.Add(new Data.Models.ProductInventoryData
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10

                });
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.ProductData>
                {
                    Data = product,
                    Message = "Product was added successfully!",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.ProductData>
                {
                    Data = product,
                    Message = $"{e}Error Saving changes",
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };

            }

        }

        public List<Data.Models.ProductData> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public Data.Models.ProductData GetProductById(int id)
        {
            return _db.Products.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
