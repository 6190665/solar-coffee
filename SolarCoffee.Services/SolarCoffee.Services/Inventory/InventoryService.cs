using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly Data.SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(Data.SolarDbContext db,ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi =>pi.Product)
                .Where(pi =>!pi.Product.IsArchived)
                .ToList();
        }
     
        public ProductInventory GetProductId(int productId)
        {
            return _db.ProductInventories.Find(productId);
        }

        public List<ProductInventorySnapshot> GetSnapShotHistory()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates number of units available of the provided product id 
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added/removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                     .Include(inv => inv.Product)
                     .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;
                try
                {
                    CreateSnapshot();
                }
                catch (Exception e) {
                    _logger.LogError("Error creating inventory Snapshot.");
                }
                _db.SaveChanges();
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = e.StackTrace + $": Error updating ProductInventory QuantityOnHand",
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
