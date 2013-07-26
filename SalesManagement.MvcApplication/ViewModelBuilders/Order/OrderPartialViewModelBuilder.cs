using AutoMapper;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrderPartialViewModelBuilder
    {
        public static OrderPartialViewModel Build(Model.Order order,int clientUniqueId,int productSku,string clientFullName, string productName,float price)
        {
            var model = Mapper.Map<Model.Order, OrderPartialViewModel>(order);
            model.ClientUniqueId = clientUniqueId;
            model.ProductSku = productSku;
            model.ClientFullName = clientFullName;
            model.Price = price;
            model.ProductName = productName;
            model.TotalPrice = price*model.Amount;
            return model;
        }
    }
}