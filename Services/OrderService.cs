using System;
using System.Collections.Generic;
using Common.Helpers;
using Contracts;
using Data.Exceptions;
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
                if (!GetRepository<IClientRepository>().UniqueIdExists(newId)) break;
            }
            return newId;
        }

        public void CreateClient(Client client)
        {
            if (client == null) throw new ArgumentNullException("client");
            GetRepository<IClientRepository>().Create(client);
        }

        public bool UniqueIdExists(int id)
        {
            return GetRepository<IClientRepository>().UniqueIdExists(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return GetRepository<IClientRepository>().GetAll();
        }

        public Client GetClientByUniqueId(int clientUniqueId)
        {
            return GetRepository<IClientRepository>().GetByUniqueId(clientUniqueId);
        }

        public void EditClient(Client client)
        {
            if (client == null) throw new ArgumentNullException("client");

            var clientRepo = GetRepository<IClientRepository>();
            var oldClient = clientRepo.GetByUniqueId(client.UniqueId);
            oldClient.CopyFrom(client);
            clientRepo.Update(oldClient);
            
        }

        public void DeleteClient(int id)
        {
            GetRepository<IClientRepository>().Delete(id);;
        }

        public void CreateOrder(Order order, int productSku, string employeeLogin,int clientUniqueId)
        {
            if (order == null) throw new ArgumentNullException("order");
            if (employeeLogin == null) throw new ArgumentNullException("employeeLogin");

            var productRepo = GetRepository<IProductRepository>();
            var productId = productRepo.GetIdBySku(productSku);
            var clientId = GetRepository<IClientRepository>().GetByUniqueId(clientUniqueId).Id;
            var employeeId = GetRepository<IUserRepository>().GetByLogin(employeeLogin).Id;
            var product = productRepo.Get(productId);
            var price = product.Price;
            order.ClientId = clientId;
            order.ProductId = productId;
            order.EmployeeId = employeeId;
            order.Date = DateTime.Now;
            order.Price = price;
            order.Status = OrderStatus.Unpaid;
            GetRepository<IOrderRepository>().Create(order);
            if (product.Amount < order.Amount)
            {
                throw new DataException("Can't perform operation due to invalid data"); 
            }
            if (product.Amount == order.Amount)
            {
                product.Status = ProductStatus.TemporaryUnavailable;
            }
            product.Amount -= order.Amount;
            productRepo.Update(product);
        }

        public IEnumerable<Client> GetClientsByFullName(string firstName, string lastName)
        {
            return GetRepository<IClientRepository>().GetByFullName(firstName, lastName);
        }
    }
}
