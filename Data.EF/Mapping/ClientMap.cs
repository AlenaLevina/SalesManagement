using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            HasKey(e => e.Id);

            Property(e => e.FirstName).HasMaxLength(Client.MaxLengthFor.FirstName);
            Property(e => e.LastName).HasMaxLength(Client.MaxLengthFor.LastName);
            Property(e => e.Phone).HasMaxLength(Client.MaxLengthFor.Phone);
            Property(e => e.Email).HasMaxLength(Client.MaxLengthFor.Email);
            Property(e => e.Address).HasMaxLength(Client.MaxLengthFor.Address);

            HasMany(e => e.Orders).WithRequired(e => e.Client);
        }
    }
}
