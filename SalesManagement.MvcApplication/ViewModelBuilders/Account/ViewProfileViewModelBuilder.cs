using Model;
using SalesManagement.MvcApplication.ViewModels.Account;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Account
{
    public class ViewProfileViewModelBuilder
    {
        public static ViewProfileViewModel Build(Profile profile, string role, string login)
        {
            return new ViewProfileViewModel
            {
                DateOfBirth = profile.DateOfBirth,
                FirstName = profile.FirstName,
                Gender = profile.Gender,
                ImageUrl = profile.ImageUrl,
                LastName = profile.LastName,
                Role = role,
                Login = login
            };
        }
    }
}