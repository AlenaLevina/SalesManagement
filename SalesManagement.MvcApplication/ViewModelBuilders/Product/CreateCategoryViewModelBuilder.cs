using System.Collections.Generic;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;
using System.Linq;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CreateCategoryViewModelBuilder
    {
        public static CreateCategoryViewModel Build(IEnumerable<Characteristic> characteristics, string categoryName)
        {
            return new CreateCategoryViewModel
            {
                Name=categoryName,
                Characteristics = characteristics.Select(c=>new CreateCategoryCharacteristic{Id=c.Id,Name = c.Name}).ToList()
            };
        }
    }
}