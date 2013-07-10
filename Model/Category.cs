using System.Collections.Generic;

namespace Model
{
    public class Category: Entity<int>
    {
        public static class MaxLengthFor
        {
            public static int Name = 50;
        }


        #region Properties

        public string Name { get; set; }

        #endregion


        #region Navigation properties

        public virtual ICollection<Characteristic> Characteristics { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Category category)
        {
            Name = category.Name;
        }

        #endregion

    }
}
