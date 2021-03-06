﻿using System;

namespace Model
{
    public class Order:Entity<int>
    {
        public static class MaxLengthFor
        {
            public static int DeliveryAddress = 200;
            public static int ContactPhoneNumber = 20;
        }


        #region Properties

        public int Amount { get; set; }

        public float Price { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string ContactPhoneNumber { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime RegistrationDate { get; set; }

        #endregion


        #region Navigation properties

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int EmployeeId { get; set; }

        public virtual User Employee { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Order order)
        {
            Amount = order.Amount;
            Price = order.Price;
            DeliveryDate = order.DeliveryDate;
            DeliveryAddress = order.DeliveryAddress;
            ContactPhoneNumber = order.ContactPhoneNumber;
            Status = order.Status;
            ClientId = order.ClientId;
            Client = order.Client;
            ProductId = order.ProductId;
            Product = order.Product;
            EmployeeId = order.EmployeeId;
            Employee = order.Employee;
            RegistrationDate = order.RegistrationDate;
        }

        public float GetTotalPrice()
        {
            return Price*Amount;
        }

        #endregion

    }
}
