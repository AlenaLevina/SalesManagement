using System;
using System.Collections.Generic;

namespace Model
{
    public class Client: Entity<int>
    {
        public static class MaxLengthFor
        {
            public const int FirstName = 50;
            public const int LastName = 50;
            public const int Phone = 20;
            public const int Email = 256;
            public const int Address = 200;
        }


        #region Properties

        public int UniqueId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        #endregion


        #region Navigation properties

        public virtual ICollection<Order> Orders { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Client client)
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
            Phone = client.Phone;
            Email = client.Email;
            Address = client.Address;
            UniqueId = client.UniqueId;
        }

        #endregion


    }
}
