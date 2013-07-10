using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).HasMaxLength(Product.MaxLengthFor.Name);
        }
    }
}
