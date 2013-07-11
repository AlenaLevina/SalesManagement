﻿namespace SalesManagement.MvcApplication.ViewModels.Order
{
    public class CreateClientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? ClientId { get; set; }
        public bool Success { get; set; }
    }
}