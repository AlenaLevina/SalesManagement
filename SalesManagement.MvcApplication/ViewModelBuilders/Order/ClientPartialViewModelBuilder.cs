using System.Collections.Generic;
using System.Linq;
using Model;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientPartialViewModelBuilder
    {
        public static ClientPartialViewModel Build(IEnumerable<Client> clients, int currentClientPosition)
        {
            return new ClientPartialViewModel
            {
                Clients = clients.Select(client => new OrderClient
                    {
                        Address = client.Address,
                        Email = client.Email,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        PhoneNumber = client.Phone,
                        UniqueId = client.UniqueId
                    }),
                CurrentClientPosition = currentClientPosition
            };
        }
    }
}