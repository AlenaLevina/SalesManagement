using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).HasMaxLength(Category.MaxLengthFor.Name);
            HasMany(e => e.Characteristics).WithMany(e => e.Categories);
            HasMany(e => e.Products).WithRequired(e => e.Category);

        }
    }
}
