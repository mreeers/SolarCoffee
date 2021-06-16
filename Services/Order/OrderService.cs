using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Inventory;
using Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly DbContex _contex;
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(Data.DbContex contex, ILogger logger, IProductService productService, IInventoryService inventoryService)
        {
            _contex = contex;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach(var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                
                _inventoryService.UpdateInitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _contex.SalesOrders.Add(order);
                _contex.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Open order created",
                    Time = DateTime.UtcNow
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Gets all SalesOrders from in the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _contex.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer => customer.PrimaryAdress)
                .Include(order => order.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }

        /// <summary>
        /// Marks an open SalesOrder as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _contex.SalesOrders.Find(id);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;

            try
            {
                _contex.SalesOrders.Update(order);
                _contex.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Order {order.Id} closed: Invoice paid in full",
                    Time = DateTime.UtcNow
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
