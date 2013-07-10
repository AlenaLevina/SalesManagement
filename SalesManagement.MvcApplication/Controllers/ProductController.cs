﻿using System.Web.Mvc;
using Contracts;
using Model;
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
            return View("Category", CategoryViewModelBuilder.Build(characteristics,new Category(), ActionType.Create));
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
                var category = CategoryViewModelBuilder.Build(model);
                service.CreateCategory(category.Name, category.Characteristics);
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
                var category = CategoryViewModelBuilder.Build(model);
                service.EditCategory(category);
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

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult CreateCharacteristic(CategoryViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IProductService>();
            service.CreateCharacteristic(model.NewCharacteristicName);
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

    }
}
