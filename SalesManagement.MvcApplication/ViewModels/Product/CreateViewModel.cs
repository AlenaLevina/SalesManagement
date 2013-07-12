using System.Collections.Generic;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class CreateViewModel
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public ProductStatus Status { get; set; }
        public string Description { get; set; }
        public int Sku { get; set; }
        public Category Category { get; set; }
        public List<ProductCharacteristicValue> CharacteristicValues { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}