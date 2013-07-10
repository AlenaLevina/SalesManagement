using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class CharacteristicMap : EntityTypeConfiguration<Characteristic>
    {
        public CharacteristicMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).HasMaxLength(Characteristic.MaxLengthFor.Name);

            HasMany(e => e.CharacteristicValues).WithRequired(e => e.Characteristic);
        }
    }
}
