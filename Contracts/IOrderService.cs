﻿using System;
using System.Collections.Generic;
using Contracts.DerivedEntities;
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
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        void EditOrder(Order order, int productSku,int clientUniqueId,string employeeLogin);
        void DeleteOrder(int id);
        IEnumerable<Order> GetOrdersByEmployeeLogin(string login);
        void ChangeStatus(int orderId, OrderStatus status);
        IEnumerable<DateAmountOrderStatistic> GetMonthlyOrderAmountStatistics(string employeeLogin,DateTime afterDate);
        IDictionary<string,int> GetStatusesPercentageStatistics(string employeeLogin, DateTime afterDate);
    }
}
