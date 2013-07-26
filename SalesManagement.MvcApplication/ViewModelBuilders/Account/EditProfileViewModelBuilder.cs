using System;
using AutoMapper;
using SalesManagement.MvcApplication.ViewModels.Account;
using Profile = Model.Profile;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Account
{
    public class EditProfileViewModelBuilder
    {
        public static EditProfileViewModel Build(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException("profile");

            var model= Mapper.Map<Profile, EditProfileViewModel>(profile);
            return model;

            //return new EditProfileViewModel
            //{
            //    FirstName = profile.FirstName,
            //    LastName = profile.LastName,
            //    DateOfBirth = profile.DateOfBirth,
            //    Gender = profile.Gender,
            //    ImageUrl = profile.ImageUrl
            //};
        }

        public static Profile Build(EditProfileViewModel editProfileViewModel, string imageUrl)
        {
            if (editProfileViewModel == null) throw new ArgumentNullException("editProfileViewModel");
            var profile = Mapper.Map<EditProfileViewModel, Profile>(editProfileViewModel);
            profile.ImageUrl = imageUrl;
            return profile;
            //return new Profile
            //{
            //    DateOfBirth = editProfileViewModel.DateOfBirth,
            //    FirstName = editProfileViewModel.FirstName,
            //    Gender = editProfileViewModel.Gender,
            //    ImageUrl = imageUrl,
            //    LastName = editProfileViewModel.LastName 
            //};
        }
    }
}