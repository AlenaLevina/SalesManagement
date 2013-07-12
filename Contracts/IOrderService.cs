using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IOrderService
    {
        int GetNewId(int size);
        void CreateClient(Client client);
        bool ClientIdExists(int id);
        IEnumerable<Client> GetAllClients();
        Client GetClientByClientId(int clientId);
        void EditClient(Client client);
        void DeleteClient(int id);
    }
}
