using System;
using System.Collections.Generic;
using Common.Helpers;

namespace Model
{
    public class User:Entity<int>
    {
        public static class MaxLengthFor
        {
            public const int Login = 50;
            public const int PasswordSalt = 128;
            public const int PasswordHash = 128;
        }


        #region Properties

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        #endregion


        #region Navigation properties

        public int? ProfileId { get; set; }

        public virtual Profile Profile { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Order> RegisteredOrders { get; set; }

        #endregion


        #region Methods

        public bool IsInRole(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");

            return Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        public bool VerifyPassword(string password)
        {
            if (password == null) throw new ArgumentNullException("password");

            return PasswordHash.Equals(PasswordHelper.ComputeHash(password, PasswordSalt));
        }

        public void CopyFrom(User user)
        {
            Login = user.Login;
            Profile = user.Profile;
            PasswordHash = user.PasswordHash;
            PasswordSalt = user.PasswordSalt;
            Role = user.Role;
        }

        #endregion

    }
}
