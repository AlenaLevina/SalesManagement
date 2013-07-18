using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IOrderService
    {
        int GetNewId(int size);
        void CreateClient(Client client);
        bool UniqueIdExists(int id);
        IEnumerable<Client> GetAllClients();
        Client GetClientByUniqueId(int clientUniqueId);
        void EditClient(Client client);
        void DeleteClient(int id);
        void CreateOrder(Order order, int productSku, string employeeLogin,int clientUniqueId);
        IEnumerable<Client> GetClientsByFullName(string firstName, string lastName);
    }
}
