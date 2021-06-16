using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Serialization
{
    /// <summary>
    /// Handles mapping Order data models to and from related View Model
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps a InvoiceModel view model to a SalesOrder data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems.Select(item => new SalesOrderItem
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList(); ;

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreateOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Maps a collection of SalesOrders (data) to OrderModels (view models)
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreateOn = order.CreateOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializesSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        /// <summary>
        /// Maps a collection of SalesOrderItems (data) to SalesOrderItemModels (view models)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializesSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}
