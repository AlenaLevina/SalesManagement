using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Account
{
    public class EditProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //[DisplayName("Date of birth")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageUrl { get; set; }
    }
}