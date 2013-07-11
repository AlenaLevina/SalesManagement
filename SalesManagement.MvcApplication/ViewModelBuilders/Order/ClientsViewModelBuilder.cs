using System.Collections.Generic;
using Model;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientsViewModelBuilder
    {
        public static ClientsViewModel Build(IEnumerable<Client> clients)
        {
            return new ClientsViewModel {Clients = clients};
        }
    }
}