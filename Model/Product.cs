using System.Collections.Generic;

namespace Model
{
    public class Product:Entity<int>
    {
        public static class MaxLengthFor
        {
            public static int Name = 50;
        }

        #region Properties

        public string Name { get; set; }

        public int Amount { get; set; }

        public float Price { get; set; }

        public ProductStatus Status { get; set; }

        public string Description { get; set; }

        public int Sku { get; set; }

        #endregion


        #region Navigation properties

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<CharacteristicValue> CharacteristicValues { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(Product product)
        {
            Name = product.Name;
            Amount = product.Amount;
            Status = product.Status;
            Description = product.Description;
            CategoryId = product.CategoryId;
            Category = product.Category;
            Sku = product.Sku;
        }

        #endregion

    }
}
