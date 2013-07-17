using System.Web.Mvc;
using Contracts;
using Model;
using SalesManagement.MvcApplication.ViewModelBuilders.Product;
using SalesManagement.MvcApplication.ViewModels;
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
            return View("Category", CategoryViewModelBuilder.Build(characteristics, new Category(), ActionType.Create));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            Validate(model);
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IProductService>();
                var category = CategoryViewModelBuilder.Build(model);
                service.CreateCategory(category.Name, category.Characteristics);
                model.Success = true;
            }
            return View("Category", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult EditCategory(int id)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var category = service.GetCategoryById(id);
            var characteristics = service.GetAllCharacteristics();
            return View("Category", CategoryViewModelBuilder.Build(characteristics, category, ActionType.Edit));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult EditCategory(CategoryViewModel model)
        {
            Validate(model);
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IProductService>();
                var category = CategoryViewModelBuilder.Build(model);
                service.EditCategory(category);
                model.Success = true;
            }
            return View("Category", model);
        }

        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Categories()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var categories = service.GetAllCategories();
            return View(CategoriesViewModelBuilder.Build(categories));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult CreateCharacteristic(CategoryViewModel model)
        {
            if (model.NewCharacteristicName == null) ModelState.AddModelError("NewCharacteristicName", "Name is requiered");
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IProductService>();
                service.CreateCharacteristic(model.NewCharacteristicName);
            }
            switch (model.ActionType)
            {
                case ActionType.Create:
                    return CreateCategory();
                case ActionType.Edit:
                    return EditCategory(model.Id);
            }
            return CreateCategory();
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult DeleteCategory(int id)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            service.DeleteCategory(id);
            return Redirect(Url.Action("Categories"));
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Create()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var categories = service.GetAllCategories();
            return View("Product",ProductViewModelBuilder.Build(new Product(), categories, null,ActionType.Create));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Create(ProductViewModel model)
        {
            Validate(model);
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var categories = service.GetAllCategories();
            model.Categories = categories;
            if (ModelState.IsValid)
            {
                var product = ProductViewModelBuilder.BuildProduct(model);
                var characteristicValues = ProductViewModelBuilder.BuildCharacteristicValues(model);
                service.CreateProduct(product, characteristicValues);
                model.Success = true;
            }
            return View("Product", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Edit(int sku)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();

            var product = service.GetProductBySku(sku);
            var categories = service.GetAllCategories();
            var characteristicValues = service.GetCharacteristicValuesByProductSku(sku);
            return View("Product", ProductViewModelBuilder.Build(product, categories, characteristicValues,ActionType.Edit));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Edit(ProductViewModel model)
        {
            Validate(model);

            var service = DependencyResolver.Current.Resolve<IProductService>();
            var categories = service.GetAllCategories();
            model.Categories = categories;
            if (ModelState.IsValid)
            {
                var product = ProductViewModelBuilder.BuildProduct(model);
                service.EditProduct(product,ProductViewModelBuilder.BuildCharacteristicValues(model));
                model.Success = true;
            }
            return View("Product", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Delete(int id)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            service.DeleteProduct(id);
            return Redirect(Url.Action("Products"));
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult Products()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var products = service.GetAllProducts();
            return View(ProductsViewModelBuilder.Build(products));
        }

        #region JS actions

        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult GetCharacteristics(int categoryId)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var characteristics = service.GetCharacteristics(categoryId);
            var model = CharacteristicValuesPartialViewModelBuilder.Build(characteristics);
            return PartialView("_CharacteristicValues", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult GenerateSku()
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var newSku = service.GetNewSku(GlobalConstants.SkuLength);
            return Json(newSku, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult ProductSkuExists(int parameter)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            var exists = service.SkuExists(parameter);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Validation methods

        private void Validate(CategoryViewModel model)
        {
            if (model.Name == null) ModelState.AddModelError("Name", "Name is requiered");
            else
            {
                if (model.Name.Length > Category.MaxLengthFor.Name)
                    ModelState.AddModelError("Name", "Name is too long");
            }
        }

        private void Validate(ProductViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();

            if (model.Amount == null) ModelState.AddModelError("Amount", "Amount is requiered");
            else if (model.Amount < 0) ModelState.AddModelError("Amount", "Amount is non-negative");
            if (model.CategoryId == null) ModelState.AddModelError("CategoryId", "Choose category");
            if (model.Name == null) ModelState.AddModelError("Name", "Name is required");
            else if (model.Name.Length > Product.MaxLengthFor.Name) ModelState.AddModelError("Name", "Name is too long");
            if (model.Price == null) ModelState.AddModelError("Price", "Price is requiered");
            else if (model.Price < 0) ModelState.AddModelError("Price", "Price is non-negative");
            if (model.Sku == null) ModelState.AddModelError("Sku", "Sku is requiered");
            else if (model.ActionType == ActionType.Create && service.SkuExists(model.Sku.Value))
                ModelState.AddModelError("Sku", "Product with such sku already exists");
        }

        #endregion


    }
}
