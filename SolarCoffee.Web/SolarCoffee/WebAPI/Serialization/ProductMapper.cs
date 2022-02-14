using SolarCoffee.Data.Models;
using SolarCoffee.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.Serialization
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a Product data model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Data.Models.ProductData product) {

           var p = (new ProductModel
           {
                Id = product.Id,
               CreatedOn = product.CreatedOn,
               UpdatedOn = product.UpdatedOn,
               Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            });
            return p;
        }
        /// <summary>
        /// Maps a ProductModel view model to a Product data model 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.ProductData SerializeProductModel(ProductModel product)
        {

            return new ProductData
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}
