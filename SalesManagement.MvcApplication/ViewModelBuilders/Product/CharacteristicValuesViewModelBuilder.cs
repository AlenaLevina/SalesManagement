using System.Collections.Generic;
using System.Linq;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CharacteristicValuesViewModelBuilder
    {
        public static CharacteristicValuesViewModel Build(IEnumerable<Characteristic> characteristics)
        {
            return new CharacteristicValuesViewModel
                {
                    CharacteristicValues =
                        characteristics.Select(
                            characteristic => new ProductCharacteristicValue { CharacteristicId = characteristic.Id, CharacteristicName = characteristic.Name })
                                       .ToList()
                };
        }
    }
}