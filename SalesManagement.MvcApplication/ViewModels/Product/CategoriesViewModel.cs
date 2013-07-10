using System.Collections.Generic;
using Model;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}