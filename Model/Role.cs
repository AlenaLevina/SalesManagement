using System.Collections.Generic;

namespace Model
{
    public class Role : Entity<int>
    {
        public static class MaxLengthFor
        {
            public const int Name = 50;
        }


        #region Properties

        public string Name { get; set; }

        #endregion


        #region Navigation properties

        public virtual ICollection<User> Users { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Role role)
        {
            Name = role.Name;
            Users = role.Users;
        }

        #endregion

    }
}
