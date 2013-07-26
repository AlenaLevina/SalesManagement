using System.Collections.Generic;
using AutoMapper;
using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Product;
using System.Linq;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CategoryViewModelBuilder
    {
        public static CategoryViewModel Build(IEnumerable<Characteristic> characteristics, Category category,ActionType actionType)
        {
            var model = Mapper.Map<Category, CategoryViewModel>(category);
            model.Characteristics =
                characteristics.Select(
                    c =>
                    new CategoryCharacteristic
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Chosen = actionType != ActionType.Create && category.Characteristics.Any(e => e.Id.Equals(c.Id))
                        }).ToList();
            model.ActionType = actionType;
            return model;
        }

        public static Category Build(CategoryViewModel model)
        {
            var category = Mapper.Map<CategoryViewModel, Category>(model);
            category.Characteristics = model.Characteristics.Where(c => c.Chosen)
                                            .Select(c => new Characteristic {Id = c.Id, Name = c.Name})
                                            .ToList();
            return category;
        }
    }
}