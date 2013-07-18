using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class ClientPartialViewModel
    {
        public IEnumerable<OrderClient> Clients { get; set; }
        public int CurrentClientPosition { get; set; }
    }
}