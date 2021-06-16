using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly DbContex _contex;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(Data.DbContex contex, ILogger<InventoryService> logger)
        {
            _contex = contex;
            _logger = logger;
        }

        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productId)
        {
            return _contex.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        /// <summary>
        /// Retutns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _contex.ProductInventories.Include(pi => pi.Product).Where(pi => !pi.Product.IsArchived).ToList();
        }

        /// <summary>
        /// Return Snapshot history for the previous 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _contex.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjust QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added / removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateInitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _contex.ProductInventories.Include(inv => inv.Product).First(inv => inv.Product.Id == id);
                inventory.QuantityOnHand += adjustment;
                _contex.SaveChanges();

                try
                {
                    CreateSnapshot(inventory);
                }
                catch(Exception ex)
                {
                    _logger.LogError("Error creating inventory snapshot.");
                    _logger.LogError(ex.StackTrace);
                }

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.UtcNow
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"Error updating ProductInventory QuantityOnHand",
                    Time = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Create a Snapshot record using the provided ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _contex.Add(snapshot);
        }
    }
}
