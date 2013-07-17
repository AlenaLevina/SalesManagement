using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Common.Helpers;
using Contracts;
using Model;
using SalesManagement.MvcApplication.ViewModelBuilders.Order;
using SalesManagement.MvcApplication.ViewModels;
using SalesManagement.MvcApplication.ViewModels.Order;
using DependencyResolver = Common.Dependency.DependencyResolver;

namespace SalesManagement.MvcApplication.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult Clients()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var clients = service.GetAllClients();
            return View(ClientsViewModelBuilder.Build(clients));
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult CreateClient()
        {
            return View("Client", ClientViewModelBuilder.Build(new Client(), ActionType.Create));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult CreateClient(ClientViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IOrderService>();
                var client = ClientViewModelBuilder.Build(model);
                service.CreateClient(client);
                model.Success = true;
            }
            return View("Client", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult EditClient(int id)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(id);
            return View("Client", ClientViewModelBuilder.Build(client, ActionType.Edit));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult EditClient(ClientViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IOrderService>();
                var client = ClientViewModelBuilder.Build(model);
                service.EditClient(client);
                model.Success = true;
            }
            return View("Client", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult DeleteClient(int id)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            service.DeleteClient(id);
            return Redirect(Url.Action("Clients"));
        }

        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Orders()
        {
            //TODO create View "Orders"
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult Create()
        {
            return View("Order",OrderViewModelBuilder.Build(new Order(), ActionType.Create,null,null));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult Create(OrderViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IOrderService>();
                var order = OrderViewModelBuilder.Build(model);
                var sku = model.ProductSku.Value;
                var clientUniqueId = model.ClientUniqueId.Value;
                var employeeLogin = User.Identity.Name;
                service.CreateOrder(order, sku, employeeLogin,clientUniqueId);
                model.Success = true;
                return Redirect(Url.Action("Confirm", model));
            }
            return View("Order", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult Confirm(OrderViewModel model)
        {
            return View(model);
        }

        //TODO Get and Post method for Edit()

        #region JS actions

        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult GenerateId()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var newId = service.GetNewId(GlobalConstants.ClientIdLength);
            return Json(newId, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult ClientUniqueIdExists(int parameter)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var exists = service.UniqueIdExists(parameter);
            return Json(exists,JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult GetClient(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(uniqueId);
            var model = ClientPartialViewModelBuilder.Build(client);
            return PartialView("_Client", model);
        }
        
        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetClientAddress(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(uniqueId);
            if (client != null) return Json(client.Address, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetClientPhone(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(uniqueId);
            if (client != null) return Json(client.Phone, JsonRequestBehavior.AllowGet);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Validation methods

        private void Validate(ClientViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            if (model.ClientId == null) ModelState.AddModelError("ClientId", "Please, generate client id");
            else if (model.ActionType == ActionType.Create && service.UniqueIdExists(model.ClientId.Value))
                ModelState.AddModelError("ClientId", "Such client id already exists");
            if (model.FirstName == null) ModelState.AddModelError("FirstName", "First name is requiered");
            else if (model.FirstName.Length > Client.MaxLengthFor.FirstName)
                ModelState.AddModelError("FirstName", "First name is too long");
            if (model.LastName == null) ModelState.AddModelError("LastName", "Last name is requiered");
            else if (model.LastName.Length > Client.MaxLengthFor.LastName)
                ModelState.AddModelError("LastName", "Last name is too long");
            if (model.Phone == null) ModelState.AddModelError("Phone", "Phone number is requiered");
            else if (!PhoneNumberHelper.IsPhoneNumber(model.Phone))
                ModelState.AddModelError("Phone", "Phone number can contain only digits and hyphens \"-\"");
            else if (model.Phone.Length > Client.MaxLengthFor.Phone)
                ModelState.AddModelError("Phone", "Phone number is too long");
            if (model.Address == null) ModelState.AddModelError("Address", "Address is requiered");
            else if (model.Address.Length > Client.MaxLengthFor.Address)
                ModelState.AddModelError("Address", "Address is too long");
            if (model.Email == null) ModelState.AddModelError("Email", "Email is requiered");
            else
            {
                if (model.Email.Length > Client.MaxLengthFor.Email)
                    ModelState.AddModelError("Email", "Email is too long");
                var regex =
                    new Regex(
                        @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
                if (!regex.IsMatch(model.Email)) ModelState.AddModelError("Email", "Wrong e-mail format");
            }
        }

        private void Validate(OrderViewModel model)
        {
            var orderService = DependencyResolver.Current.Resolve<IOrderService>();
            var productService = DependencyResolver.Current.Resolve<IProductService>();

            if (model.ClientUniqueId == null) ModelState.AddModelError("ClientUniqueId", "Client Id is requiered");
            else if (model.ClientUniqueId.Value < 0) ModelState.AddModelError("ClientUniqueId", "Client Id is non-negative");
            else if (!orderService.UniqueIdExists(model.ClientUniqueId.Value)) ModelState.AddModelError("ClientUniqueId", "There is no client with such id");

            if (model.ProductSku == null) ModelState.AddModelError("ProductSku", "Product SKU is requiered");
            else if (model.ProductSku.Value < 0) ModelState.AddModelError("ProductSku", "Product SKU is non-negative");
            else if (!productService.SkuExists(model.ProductSku.Value)) ModelState.AddModelError("ProductSku", "There is no product with such SKU");

            if (model.Amount == null) ModelState.AddModelError("Amount", "Amount is requiered");
            else if (model.Amount.Value < 0) ModelState.AddModelError("Amount", "Amount is non-negative");

            if (model.DeliveryDate == null) ModelState.AddModelError("DeliveryDate", "Delivery date is requiered");
            else if (model.DeliveryDate.Value < DateTime.Now) ModelState.AddModelError("DeliveryDate", "Choose delivery date after " + DateTime.Now);

            if (model.DeliveryAddress == null) ModelState.AddModelError("DeliveryAddress", "Delivery address is requiered");
            else if (model.DeliveryAddress.Length > Order.MaxLengthFor.DeliveryAddress) ModelState.AddModelError("DeliveryAddress", "Delivery address is too long");

            if (model.ContactPhoneNumber == null) ModelState.AddModelError("ContactPhoneNumber", "Phone number is requiered");
            else if (model.ContactPhoneNumber.Length > Order.MaxLengthFor.ContactPhoneNumber) ModelState.AddModelError("ContactPhoneNumber","Phone number is too long");
            else if (!PhoneNumberHelper.IsPhoneNumber(model.ContactPhoneNumber)) ModelState.AddModelError("ContactPhoneNumber","Wrong formut of phone number");
        }

        #endregion

    }
}
