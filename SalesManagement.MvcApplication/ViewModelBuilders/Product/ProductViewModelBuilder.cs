using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Model;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class ProductViewModelBuilder
    {
        public static ProductViewModel Build(Model.Product product, IEnumerable<Category> categories, IEnumerable<CharacteristicValue> characteristicValues,ActionType actionType)
        {
            var model = Mapper.Map<Model.Product, ProductViewModel>(product);
            model.Amount = (actionType==ActionType.Create)&&(product.Amount == default(int)) ? (int?)null : product.Amount;
            model.Categories = categories;
            model.CategoryId = product.CategoryId == default(int) ? (int?)null : product.CategoryId;
            model.Price = product.Price.Equals(default(float)) ? (float?)null : product.Price;
            model.Sku = product.Sku == default(int) ? (int?)null : product.Sku;
            model.ActionType = actionType;
            model.CharacteristicValues = characteristicValues == null ? null : characteristicValues.ToList().Select(characteristicValue => new ProductCharacteristicValue { CharacteristicId = characteristicValue.CharacteristicId, CharacteristicName = characteristicValue.Characteristic.Name, Value = characteristicValue.Value, Id = characteristicValue.Id}).ToList();
            return model;
        }

        //public static ProductViewModel Build(Model.Product product, IEnumerable<Category> categories, IEnumerable<ProductCharacteristicValue> productCharacteristicValues)
        //{
        //    return new ProductViewModel
        //    {
        //        Amount = product.Amount == default(int) ? (int?)null : product.Amount,
        //        Categories = categories,
        //        CategoryId = product.CategoryId == default(int) ? (int?)null : product.CategoryId,
        //        Description = product.Description,
        //        Name = product.Name,
        //        Price = product.Price.Equals(default(float)) ? (float?)null : product.Price,
        //        Sku = product.Sku == default(int) ? (int?)null : product.Sku,
        //        Status = product.Status,
        //        CharacteristicValues = productCharacteristicValues.ToList()
        //    };
        //}

        public static IEnumerable<CharacteristicValue> BuildCharacteristicValues(ProductViewModel model)
        {
            return
                model.CharacteristicValues.Select(
                    charValue =>
                    new CharacteristicValue { CharacteristicId = charValue.CharacteristicId, Value = charValue.Value,Id = charValue.Id});
        }

        public static Model.Product BuildProduct(ProductViewModel model)
        {
            return Mapper.Map<ProductViewModel, Model.Product>(model);
        }
    }
}