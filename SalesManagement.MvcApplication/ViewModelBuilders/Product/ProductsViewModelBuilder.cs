using System.Collections.Generic;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class ProductsViewModelBuilder
    {
        public static ProductsViewModel Build(IEnumerable<Model.Product> products)
        {
            return new ProductsViewModel {Products = products};
        }
    }
}