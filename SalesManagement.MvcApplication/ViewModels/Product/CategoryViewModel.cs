using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryCharacteristic> Characteristics { get; set; }
        public bool Success { get; set; }
        public ActionType ActionType { get; set; }
        public string NewCharacteristicName { get; set; }
    }
}