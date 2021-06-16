using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateInitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
