using System.Collections.Generic;

namespace Model
{
    public class Client: Entity<int>
    {
        public static class MaxLengthFor
        {
            public const int FirstName = 50;
            public const int LastName = 50;
        }


        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion


        #region Navigation properties

        public virtual ICollection<Order> Orders { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Client client)
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
        }

        #endregion


    }
}
