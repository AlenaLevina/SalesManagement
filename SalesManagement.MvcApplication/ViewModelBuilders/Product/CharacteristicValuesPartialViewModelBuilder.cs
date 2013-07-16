using System.Collections.Generic;
using System.Linq;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CharacteristicValuesPartialViewModelBuilder
    {
        public static CharacteristicValuesPartialViewModel Build(IEnumerable<Characteristic> characteristics)
        {
            return new CharacteristicValuesPartialViewModel
                {
                    CharacteristicValues =
                        characteristics.Select(
                            characteristic => new ProductCharacteristicValue { CharacteristicId = characteristic.Id, CharacteristicName = characteristic.Name })
                                       .ToList()
                };
        }

        public static CharacteristicValuesPartialViewModel Build(
            List<ProductCharacteristicValue> productCharacteristicValues)
        {
            return new CharacteristicValuesPartialViewModel
                {
                    CharacteristicValues = productCharacteristicValues
                };
        }
    }
}