using SolarCoffee.Data.Models;
using SolarCoffee.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.Serialization
{
    public static class SalesOrderMapper
    {
        /// <summary>
        /// Maping order data models to and from related  view models
        /// </summary>
        /// <param name="salesOrder"></param>
        /// <returns></returns>
        public static SalesOrderModel SerializeOrderModel(SalesOrderData salesOrder)
        {
            DateTime now = DateTime.UtcNow;
            var SalesOrderItemList = salesOrder.SalesOrderItems
                .Select(item => new SalesOrderItemModel
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
            var p = (new SalesOrderModel
            {
                Id = salesOrder.Id,
                CreatedOn = salesOrder.CreatedOn,
                UpdatedOn =now,
                IsPaid = salesOrder.IsPaid,
                SalesOrderItems = SalesOrderItemList

            });
            return p;
        }
        /// <summary>
        /// Maps a SalesOrderModel view model to a SalesOrder data model 
        /// </summary>
        /// <param name="salesOrder"></param>
        /// <returns></returns>
        public static Data.Models.SalesOrderData SerializeOrderModel(SalesOrderModel invoice)
        {
            DateTime now = DateTime.UtcNow;
            var SalesOrderItemList = invoice.SalesOrderItems
                .Select(item => new SalesOrderItemData
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();
            return (new SalesOrderData
            {
                Id = invoice.Id,
                CreatedOn = invoice.CreatedOn,
                UpdatedOn = invoice.UpdatedOn,
                IsPaid = invoice.IsPaid,
                SalesOrderItems = SalesOrderItemList

            }); 
        }
        public static List<SalesOrderModel> SerializeOrderModelToViewModels(IEnumerable<SalesOrderData> orders)
        {
            return orders.Select(order => new SalesOrderModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                IsPaid = order.IsPaid,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomerModel(order.Cutomer)
                
            }).ToList();
        }
        /// <summary>
        /// Maps a collection of SalesOrder (data) to SalesOrderItemModels(view models)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItemData> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel{
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)

            }).ToList();
        }
    }

}
