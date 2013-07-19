using System;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class OrderPartialViewModel
    {
        public int ClientUniqueId { get; set; }
        public int ProductSku { get; set; }
        public int Amount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}