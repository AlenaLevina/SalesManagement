using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrderPartialViewModelBuilder
    {
        public static OrderPartialViewModel Build(Model.Order order,int clientUniqueId,int productSku)
        {
            return new OrderPartialViewModel
                {
                    Amount = order.Amount,
                    ClientUniqueId = clientUniqueId,
                    ContactPhoneNumber = order.ContactPhoneNumber,
                    DeliveryAddress = order.DeliveryAddress,
                    DeliveryDate = order.DeliveryDate,
                    ProductSku = productSku
                };
        }
    }
}