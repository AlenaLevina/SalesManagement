using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class ProductPartialViewModel
    {
        public IEnumerable<OrderProduct> Products { get; set; }
        public int CurrentProductPosition { get; set; }
    }
}