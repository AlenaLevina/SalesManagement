using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }
        public List<CreateCategoryCharacteristic> Characteristics { get; set; }
        public bool SuccesfullyCreated { get; set; }
    }
}