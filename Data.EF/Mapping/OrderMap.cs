using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.EF.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(e => e.Id);

            Property(e => e.DeliveryAddress).HasMaxLength(Order.MaxLengthFor.DeliveryAddress);

            HasRequired(e => e.Product).WithMany(e => e.Orders);
            HasRequired(e => e.Employee).WithMany(e => e.RegisteredOrders);
        }
    }
}
