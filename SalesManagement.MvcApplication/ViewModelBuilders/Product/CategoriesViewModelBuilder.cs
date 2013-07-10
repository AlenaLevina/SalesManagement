using System.Collections.Generic;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;


namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CategoriesViewModelBuilder
    {
        public static CategoriesViewModel Build(IEnumerable<Category> categories)
        {
            return new CategoriesViewModel {Categories = categories};
        }
    }
}