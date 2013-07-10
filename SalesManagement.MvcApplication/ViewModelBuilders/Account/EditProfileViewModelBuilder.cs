using System;
using Model;
using SalesManagement.MvcApplication.ViewModels.Account;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Account
{
    public class EditProfileViewModelBuilder
    {
        public static EditProfileViewModel Build(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException("profile");

            return new EditProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                ImageUrl = profile.ImageUrl
            };
        }

        public static Profile Build(EditProfileViewModel editProfileViewModel, string imageUrl)
        {
            if (editProfileViewModel == null) throw new ArgumentNullException("editProfileViewModel");
            return new Profile
            {
                DateOfBirth = editProfileViewModel.DateOfBirth,
                FirstName = editProfileViewModel.FirstName,
                Gender = editProfileViewModel.Gender,
                ImageUrl = imageUrl,
                LastName = editProfileViewModel.LastName
            };
        }
    }
}