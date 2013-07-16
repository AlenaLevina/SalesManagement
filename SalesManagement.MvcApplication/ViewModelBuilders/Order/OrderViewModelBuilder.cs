using System;
using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrderViewModelBuilder
    {
        public static OrderViewModel Build(Model.Order order, ActionType actionType,int? productSku,int? clientUniqueId)
        {
            return new OrderViewModel
                {
                    Amount = order.Amount == default(int) ? (int?)null : order.Amount,
                    ClientUniqueId = clientUniqueId,
                    ProductSku =  productSku,
                    DeliveryDate = order.DeliveryDate == default(DateTime) ? (DateTime?)null : order.DeliveryDate,
                    DeliveryAddress = order.DeliveryAddress,
                    ContactPhoneNumber = order.ContactPhoneNumber,
                    ActionType = actionType
                };
        }

        public static Model.Order Build(OrderViewModel model)
        {
            return new Model.Order
                {
                    Amount = model.Amount.Value,
                    DeliveryDate = model.DeliveryDate.Value,
                    DeliveryAddress = model.DeliveryAddress,
                    ContactPhoneNumber = model.ContactPhoneNumber
                };
        }
    }
}