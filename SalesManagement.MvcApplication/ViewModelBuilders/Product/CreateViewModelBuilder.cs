using System.Collections.Generic;
using Model;
using SalesManagement.MvcApplication.ViewModels.Product;

namespace SalesManagement.MvcApplication.ViewModelBuilders.Product
{
    public class CreateViewModelBuilder
    {
        public static CreateViewModel Build(Model.Product product,IEnumerable<Category> categories)
        {
            return new CreateViewModel
                {
                    Amount = product.Amount,
                    Categories = categories,
                    Category = product.Category,
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Sku = product.Sku,
                    Status = product.Status
                };
        }
    }
}