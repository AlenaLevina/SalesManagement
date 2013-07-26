using AutoMapper;
using SalesManagement.MvcApplication.ViewModels.Account;
using Profile = Model.Profile;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Account
{
    public class ViewProfileViewModelBuilder
    {
        public static ViewProfileViewModel Build(Profile profile, string role, string login)
        {
            var model=Mapper.Map<Profile, ViewProfileViewModel>(profile);
            model.Role = role;
            model.Login = login;
            return model;
        }
    }
}