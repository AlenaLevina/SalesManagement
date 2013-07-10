using System.Collections.Generic;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;
using System.Linq;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CategoryViewModelBuilder
    {
        public static CategoryViewModel Build(IEnumerable<Characteristic> characteristics, Category category,ActionType actionType)
        {
            return new CategoryViewModel
            {
                Id=category.Id,
                Name=category.Name,
                Characteristics = characteristics.Select(c=>new CategoryCharacteristic{Id=c.Id,Name = c.Name,Chosen =  actionType != ActionType.Create && category.Characteristics.Any(e => e.Id.Equals(c.Id))}).ToList(),
                ActionType = actionType
            };
        }

        public static Category Build(CategoryViewModel model)
        {
            var category = new Category
                {
                    Characteristics =
                        model.Characteristics.Where(c => c.Chosen)
                             .Select(c => new Characteristic {Id = c.Id, Name = c.Name})
                             .ToList(),
                    Name = model.Name,
                    Id=model.Id
                };
            return category;
        }
    }
}