using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            HasKey(e => e.Id);

            Property(e => e.FirstName).HasMaxLength(Profile.MaxLengthFor.FirstName);
            Property(e => e.LastName).HasMaxLength(Profile.MaxLengthFor.LastName);

            //HasRequired(e => e.User).WithOptional(e => e.Profile);
        }
    }
}
