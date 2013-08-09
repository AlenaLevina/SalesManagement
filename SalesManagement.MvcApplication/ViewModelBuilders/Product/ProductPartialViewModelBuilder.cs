using System.Collections.Generic;
using System.Linq;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class ProductPartialViewModelBuilder
    {
        public static ProductPartialViewModel Build(IEnumerable<Model.Product> products, int currentProductPosition)
        {
            return new ProductPartialViewModel
                {
                    Products=products.ToList().Select(p=>new OrderProduct
                        {
                            CategoryName = p.Category.Name,
                            CharacteristicValues = p.CharacteristicValues.ToList().Select(cv=>new ProductCharacteristicValue
                                {
                                    CharacteristicId = cv.CharacteristicId,
                                    CharacteristicName = cv.Characteristic.Name,
                                    Id=cv.Id,
                                    Value = cv.Value
                                }),
                                Description = p.Description,
                                Name = p.Name,
                                Price = p.Price,
                                Sku = p.Sku,
                                Amount = p.Amount
                        }),
                        CurrentProductPosition = currentProductPosition
                };
        }
    }
}