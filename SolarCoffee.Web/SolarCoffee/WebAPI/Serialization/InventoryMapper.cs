using SolarCoffee.Data.Models;
using SolarCoffee.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.Serialization
{
    public static class InventoryMapper
    {
        /// <summary>
        /// Maps a Product data model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductInventoryModel SerializeInventoryModel(ProductInventoryData proInventory) {

           var p = (new ProductInventoryModel
           {
                Id = proInventory.Id,
               CreatedOn = proInventory.CreatedOn,
               UpdatedOn = proInventory.UpdatedOn,
               IdealQuantity = proInventory.IdealQuantity,
               //Product=proInventory.Product,
               QuantityOnHand= proInventory.QuantityOnHand
           });
            return p;
        }
        /// <summary>
        /// Maps a ProductModel view model to a Product data model 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductInventoryData SerializeInventoryModel(ProductInventoryModel proInventory)
        {

            return new ProductInventoryData
            {
                Id = proInventory.Id,
                CreatedOn = proInventory.CreatedOn,
                UpdatedOn = proInventory.UpdatedOn,
                IdealQuantity = proInventory.IdealQuantity,
                //Product=proInventory.Product,
                QuantityOnHand = proInventory.QuantityOnHand
            };
        }
    }
}
