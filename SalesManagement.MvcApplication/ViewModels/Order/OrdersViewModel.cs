using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class OrdersViewModel
    {
        public IEnumerable<Model.Order> Orders { get; set; }
    }
}