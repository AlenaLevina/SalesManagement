using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Client Id is requiered")]
        [Range(10*GlobalConstants.ClientIdLength,10*(GlobalConstants.ClientIdLength+1)-1,ErrorMessage = "Wrong format of client unique ID")]
        public int? ClientUniqueId { get; set; }//
        public int? ProductSku { get; set; } //
        public int? Amount { get; set; }
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeFormat, ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
        public int OldAmount { get; set; }
        public OrderStatus Status { get; set; }
        public int? Id { get; set; }
        public float? Price { get; set; }

        /*public bool ValidationOnly { get; set; }*/
        public bool Success { get; set; }
        public ActionType ActionType { get; set; }
    }
}