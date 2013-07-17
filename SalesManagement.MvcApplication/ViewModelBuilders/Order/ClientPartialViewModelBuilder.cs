using Model;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientPartialViewModelBuilder
    {
        public static ClientPartialViewModel Build(Client client)
        {
            return new ClientPartialViewModel
                {
                    Address = client.Address,
                    Email = client.Email,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    PhoneNumber = client.Phone,
                    UniqueId = client.UniqueId
                };
        }
    }
}