using System;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers;
using Contracts;
using Contracts.DerivedEntities;
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
            order.RegistrationDate = DateTime.Now;
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

        public Order GetOrderById(int id)
        {
            return GetRepository<IOrderRepository>().Get(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return GetRepository<IOrderRepository>().GetAll();
        }

        public void EditOrder(Order order, int productSku, int clientUniqueId, string employeeLogin)
        {
            if (order == null) throw new ArgumentNullException("order");
            var orderRepo = GetRepository<IOrderRepository>();
            var oldOrder = orderRepo.Get(order.Id);
            oldOrder.CopyFrom(order);
            var client = GetRepository<IClientRepository>().GetByUniqueId(clientUniqueId);
            oldOrder.Client = client;
            oldOrder.ClientId = client.Id;
            var product = GetRepository<IProductRepository>().GetBySku(productSku);
            oldOrder.Product = product;
            oldOrder.ProductId = product.Id;
            var user = GetRepository<IUserRepository>().GetByLogin(employeeLogin);
            oldOrder.Employee = user;
            oldOrder.EmployeeId = user.Id;
            orderRepo.Update(oldOrder);
        }

        public void DeleteOrder(int id)
        {
            GetRepository<IOrderRepository>().Delete(id);
        }

        public IEnumerable<Order> GetOrdersByEmployeeLogin(string login)
        {
            if (login == null) throw new ArgumentNullException("login");

            var employeeId = GetRepository<IUserRepository>().GetByLogin(login).Id;
            return GetRepository<IOrderRepository>().GetAllByEmployeeId(employeeId);
        }

        public void ChangeStatus(int orderId, OrderStatus status)
        {
            var orderRepo = GetRepository<IOrderRepository>();
            var order = orderRepo.Get(orderId);
            order.Status = status;
            orderRepo.Update(order);
        }

        public IEnumerable<DateAmountOrderStatistic> GetMonthlyOrderAmountStatistics(string employeeLogin,DateTime afterDate)
        {
            if (employeeLogin == null) throw new ArgumentNullException("employeeLogin");
            afterDate=afterDate.AddDays(1 - afterDate.Day);
            var employeeId = GetRepository<IUserRepository>().GetByLogin(employeeLogin).Id;
            var orders = GetRepository<IOrderRepository>().GetAllByEmployeeId(employeeId, afterDate);
            return
                orders.Where(order=>order.Status==OrderStatus.Delivered||order.Status==OrderStatus.Paid)
                      .GroupBy(order => new Tuple<int, int>(order.RegistrationDate.Month, order.RegistrationDate.Year))
                      .Select(
                          groupedOrders =>
                          new DateAmountOrderStatistic
                              {
                                  Date = new DateTime(groupedOrders.Key.Item2, groupedOrders.Key.Item1, 1),
                                  OrderAmount = groupedOrders.Count()
                              }).OrderByDescending(o => o.Date);
        }

        public IDictionary<string, int> GetStatusesPercentageStatistics(string employeeLogin, DateTime afterDate)
        {
            if (employeeLogin == null) throw new ArgumentNullException("employeeLogin");
            afterDate=afterDate.AddDays(1 - afterDate.Day);
            var employeeId = GetRepository<IUserRepository>().GetByLogin(employeeLogin).Id;
            var orders = GetRepository<IOrderRepository>().GetAllByEmployeeId(employeeId, afterDate);
            var result = orders.GroupBy(order => order.Status).ToDictionary(groupedOrders => groupedOrders.Key.ToString(), groupedOrders=>groupedOrders.Count());
            return result;

        }
    }
}
