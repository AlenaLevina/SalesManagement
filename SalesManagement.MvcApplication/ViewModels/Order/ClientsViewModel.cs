using System.Collections.Generic;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class ClientsViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
    }
}