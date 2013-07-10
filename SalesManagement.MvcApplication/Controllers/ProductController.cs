using System.Web.Mvc;
using Contracts;
using SalesManagement.MvcApplication.ViewModelBuilders.Product;
using SalesManagement.MvcApplication.ViewModels.Product;
using DependencyResolver = Common.Dependency.DependencyResolver;

namespace SalesManagement.MvcApplication.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult CreateCategory()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var characteristics = service.GetAllCharacteristics();
            return View("Category", CategoryViewModelBuilder.Build(characteristics,null,ActionType.Create));
        }
        
        [HttpPost]
        [Authorize (Roles = RoleNames.AdministratorRoleName)]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            if (model.Name == null) ModelState.AddModelError("Name", "Name is requiered");
            else
            {
                if (model.Name.Length > Model.Category.MaxLengthFor.Name) ModelState.AddModelError("Name", "Name is too long");
            }
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IProductService>();
                var chosenCharacteristics = CategoryViewModelBuilder.Build(model);
                service.CreateCategory(model.Name, chosenCharacteristics);
                model.Success = true;
                model.ActionType=ActionType.Create;
            }
            return View("Category",model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult EditCategory(int id)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var category = service.GetCategoryById(id);
            var characteristics = service.GetAllCharacteristics();
            return View("Category", CategoryViewModelBuilder.Build(characteristics, category,ActionType.Edit));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult EditCategory(CategoryViewModel model)
        {
            if (model.Name == null) ModelState.AddModelError("Name", "Name is requiered");
            else
            {
                if (model.Name.Length > Model.Category.MaxLengthFor.Name) ModelState.AddModelError("Name", "Name is too long");
            }
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IProductService>();
                var chosenCharacteristics = CategoryViewModelBuilder.Build(model);
                service.EditCategory(model.Id, chosenCharacteristics);
                model.Success = true;
                model.ActionType=ActionType.Edit;
            }
            return View("Category", model);
        }

        public ActionResult Categories()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var categories = service.GetAllCategories();
            return View(CategoriesViewModelBuilder.Build(categories));
        }
    }
}
