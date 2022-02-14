using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventoryData> GetCurrentInventory();
        public ServiceResponse<ProductInventoryData> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventoryData GetByProductId(int productId);
        //public void CreateSnapshot(ProductInventory inventory);
        public List<ProductInventorySnapshotData> GetSnapShotHistory();


    }
}
