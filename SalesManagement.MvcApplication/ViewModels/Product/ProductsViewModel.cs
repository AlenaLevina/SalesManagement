using System.Collections.Generic;

namespace SalesManagement.MvcApplication.ViewModels.Product
{
    public class ProductsViewModel
    {
        public IEnumerable<Model.Product> Products { get; set; }
    }
}