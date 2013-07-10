using System.Collections.Generic;

namespace Model
{
    public class Characteristic : Entity<int>
    {
        public static class MaxLengthFor
        {
            public static int Name = 50;
        }


        #region Properties

        public string Name { get; set; }

        #endregion


        #region Navigation properties

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<CharacteristicValue> CharacteristicValues { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Characteristic characteristic)
        {
            Name = characteristic.Name;
            Categories = characteristic.Categories;
        }

        #endregion

    }
}
