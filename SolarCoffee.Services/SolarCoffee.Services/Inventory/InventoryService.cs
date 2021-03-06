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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot(ProductInventoryData inventory)
        {
            var now = DateTime.UtcNow;
            var snapshop = new ProductInventorySnapshotData
            {
                SnapshotTime = now,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.ProductInventorySnapshots.Add(snapshop);
        }
        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventoryData> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi =>pi.Product)
                .Where(pi =>!pi.Product.IsArchived)
                .ToList();
        }
     /// <summary>
     /// Get a ProductInventory instance by Product Id
     /// </summary>
     /// <param name="productId"></param>
     /// <returns></returns>
        public ProductInventoryData GetByProductId(int productId)
        {
            return _db.ProductInventories
                     .Include(pi => pi.Product)
                     .FirstOrDefault(pi =>pi.Product.Id == productId);
        }
        /// <summary>
        /// Return Snapshot history for the previous 6 hours        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshotData> GetSnapShotHistory()
        {
            var earliest = DateTime.UtcNow -TimeSpan.FromHours(6) ;
            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap =>snap.SnapshotTime>earliest
                                    &&!snap.Product.IsArchived)
                .ToList();
        }
        /// <summary>
        /// Updates number of units available of the provided product id 
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added/removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventoryData> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                     .Include(inv => inv.Product)
                     .First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;
                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception e) {
                    _logger.LogError($"Error creating inventory Snapshot.{e.StackTrace}");
                }
                _db.SaveChanges();
                return new ServiceResponse<ProductInventoryData>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventoryData>
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
