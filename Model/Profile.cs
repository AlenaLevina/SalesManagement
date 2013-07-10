using System;

namespace Model
{
    public class Profile : Entity<int>
    {
        public static class MaxLengthFor
        {
            public const int FirstName = 50;
            public const int LastName = 50;
        }

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ImageUrl { get; set; }

        public Gender? Gender { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Profile profile)
        {
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            DateOfBirth = profile.DateOfBirth;
            ImageUrl = profile.ImageUrl;
            Gender = profile.Gender;
        }

        #endregion


    }
}
