using System;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class OrderViewModel
    {
        public int? ClientUniqueId { get; set; }
        public int? ProductSku { get; set; }
        public int? Amount { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhoneNumber { get; set; }

        /*public bool ValidationOnly { get; set; }*/
        public bool Success { get; set; }
        public ActionType ActionType { get; set; }
    }
}