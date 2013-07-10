using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Login).HasMaxLength(User.MaxLengthFor.Login);
            Property(e => e.PasswordHash).HasMaxLength(User.MaxLengthFor.PasswordHash);
            Property(e => e.PasswordSalt).HasMaxLength(User.MaxLengthFor.PasswordSalt);

            HasOptional(e => e.Profile);
        }
    }
}
