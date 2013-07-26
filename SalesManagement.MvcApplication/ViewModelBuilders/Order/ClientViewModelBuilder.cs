using AutoMapper;
using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Order
{
    public class ClientViewModelBuilder
    {
        public static ClientViewModel Build(Client client, ActionType actionType)
        {
            var model = Mapper.Map<Client, ClientViewModel>(client);
            model.ActionType = actionType;
            model.ClientId = (client.UniqueId == default(int)) ? (int?) null : client.UniqueId;
            return model;
        }

        public static Client Build(ClientViewModel model)
        {
            var client = Mapper.Map<ClientViewModel, Client>(model);
            return client;
        }
    }
}