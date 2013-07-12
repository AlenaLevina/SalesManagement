using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers;
using Contracts;
using Data.Repositories;
using Model;

namespace Services
{
    public class OrderService:BaseService,IOrderService
    {
        public int GetNewId(int size)
        {
            int newId;
            while (true)
            {
                newId = RandomHelper.GenerateNumber(size);
                if (!GetRepository<IClientRepository>().ClientIdExists(newId)) break;
            }
            return newId;
        }

        public void CreateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException("client");
            GetRepository<IClientRepository>().Create(client);
        }

        public bool ClientIdExists(int id)
        {
            return GetRepository<IClientRepository>().ClientIdExists(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return GetRepository<IClientRepository>().GetAll();
        }

        public Client GetClientByClientId(int clientId)
        {
            return GetRepository<IClientRepository>().GetByClientId(clientId);
        }

        public void EditClient(Client client)
        {
            if (client == null) throw new ArgumentNullException("client");

            var clientRepo = GetRepository<IClientRepository>();
            var oldClient = clientRepo.GetByClientId(client.ClientId);
            oldClient.CopyFrom(client);
            clientRepo.Update(oldClient);
            
        }

        public void DeleteClient(int id)
        {
            GetRepository<IClientRepository>().Delete(id);;
        }
    }
}
