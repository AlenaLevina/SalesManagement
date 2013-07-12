using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientViewModelBuilder
    {
        public static ClientViewModel Build(Client client, ActionType actionType)
        {
            return new ClientViewModel
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                Address = client.Address,
                ClientId = (client.ClientId == default(int)) ? (int?)null : client.ClientId,
                ActionType = actionType
            };
        }

        public static Client Build(ClientViewModel model)
        {
            return new Client
                {
                    Address = model.Address,
                    ClientId = model.ClientId.Value,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone
                };
        }
    }
}