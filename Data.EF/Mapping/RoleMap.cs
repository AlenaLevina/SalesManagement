using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).HasMaxLength(Role.MaxLengthFor.Name);

            HasMany(e => e.Users).WithRequired(e => e.Role);
        }
    }
}
