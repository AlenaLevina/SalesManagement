using System.Web.Mvc;
using Contracts;
using Data.Repositories;
using SalesManagement.MvcApplication.ViewModelBuilders.Product;
using SalesManagement.MvcApplication.ViewModels.Product;
using DependencyResolver = Common.Dependency.DependencyResolver;

namespace SalesManagement.MvcApplication.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult CreateCategory()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var characteristics = service.GetAllCharacteristics();
            return View(CreateCategoryViewModelBuilder.Build(characteristics, null));
        }
        
        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            foreach (var characteristic in model.Characteristics)
            {
                int i = 0;
            }
            return View(model);
        }

    }
}
