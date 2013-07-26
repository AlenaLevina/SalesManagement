using System.Collections.Generic;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class OrdersViewModelBuilder
    {
        public static OrdersViewModel Build(IEnumerable<Model.Order> orders)
        {
            return new OrdersViewModel{Orders = orders};
        }
    }
}