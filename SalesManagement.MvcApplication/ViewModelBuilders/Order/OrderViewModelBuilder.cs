using System;
using AutoMapper;
using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrderViewModelBuilder
    {
        public static OrderViewModel Build(Model.Order order, ActionType actionType,int? productSku,int? clientUniqueId)
        {
            var model = Mapper.Map<Model.Order, OrderViewModel>(order);
            model.Amount = order.Amount == default(int) ? (int?) null : order.Amount;
            model.ClientUniqueId = clientUniqueId;
            model.ProductSku =  productSku;
            model.DeliveryDate = order.DeliveryDate == default(DateTime) ? (DateTime?)null : order.DeliveryDate;
            model.ActionType = actionType;
            model.OldAmount = order.Amount;
            return model;
        }

        public static Model.Order Build(OrderViewModel model)
        {
            return Mapper.Map<OrderViewModel, Model.Order>(model);
        }
    }
}