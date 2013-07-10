using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class CharacteristicValueMap : EntityTypeConfiguration<CharacteristicValue>
    {
        public CharacteristicValueMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Value).HasMaxLength(CharacteristicValue.MaxLengthFor.Value);
            
            HasRequired(e => e.Product).WithMany(e => e.CharacteristicValues);

        }
    }
}
