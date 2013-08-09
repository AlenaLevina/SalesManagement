using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class OrderProduct
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ProductCharacteristicValue> CharacteristicValues { get; set; }
        public string Description { get; set; }
        public int Sku { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
    }
}