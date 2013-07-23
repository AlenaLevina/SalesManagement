using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrderPartialViewModelBuilder
    {
        public static OrderPartialViewModel Build(Model.Order order,int clientUniqueId,int productSku,string clientFullName, string productName,float price)
        {
            return new OrderPartialViewModel
                {
                    Amount = order.Amount,
                    ClientUniqueId = clientUniqueId,
                    ContactPhoneNumber = order.ContactPhoneNumber,
                    DeliveryAddress = order.DeliveryAddress,
                    DeliveryDate = order.DeliveryDate,
                    ProductSku = productSku,
                    ClientFullName = clientFullName,
                    Price = price,
                    ProductName = productName,
                    TotalPrice = price*order.Amount
                };
        }
    }
}