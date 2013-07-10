using System;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Account
{
    public class ViewProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public Gender? Gender { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
    }
}