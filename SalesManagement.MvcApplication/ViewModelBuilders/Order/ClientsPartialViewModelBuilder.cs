using Model;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientsPartialViewModelBuilder
    {
        public static ClientsPartialViewModel Build(Client client)
        {
            return new ClientsPartialViewModel
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