using System;
using System.Collections.Generic;
using System.Linq;
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
//#if DEBUG
//        public ActionResult Test()
//        {
//            var model = new OrderViewModel
//                {
//                    ActionType = ActionType.Create,
//                    Amount = 4,
//                    ClientUniqueId = 1227352,
//                    ContactPhoneNumber = "+375-44-521-12-89",
//                    DeliveryAddress = "New Orlean",
//                    DeliveryDate = DateTime.Now,
//                    ProductSku = 61051089
//                };
//            return GetOrderSummary(model);
//        }
//#endif

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
        public ActionResult EditClient(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(uniqueId);
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
        public ActionResult DeleteClient(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            service.DeleteClient(service.GetClientByUniqueId(uniqueId).Id);
            return Redirect(Url.Action("Clients"));
        }

        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult All()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var orders = service.GetAllOrders();
            var model = OrdersViewModelBuilder.Build(orders);
            return View("Orders",model);
        }

        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult My()
        {
            var employeeLogin = User.Identity.Name;
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var orders = service.GetOrdersByEmployeeLogin(employeeLogin).ToList();
            var model = OrdersViewModelBuilder.Build(orders);
            return View("Orders", model);

        }

        [HttpGet]
        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult Create()
        {
            return View("Order", OrderViewModelBuilder.Build(new Order(), ActionType.Create, null, null));
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
                service.CreateOrder(order, sku, employeeLogin, clientUniqueId);
                model.Success = true;
                return Redirect(Url.Action("Confirm", model));
            }
            return View("Order", model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.EmployeeActionsRoleName)]
        public ActionResult Confirm(OrderViewModel model)
        {
            var orderService = DependencyResolver.Current.Resolve<IOrderService>();
            var productService = DependencyResolver.Current.Resolve<IProductService>();
            var client = orderService.GetClientByUniqueId(model.ClientUniqueId.Value);
            var clientFullName = client.FirstName + " " + client.LastName;
            var product = productService.GetProductBySku(model.ProductSku.Value);
            var productName = product.Name;
            var productPrice = product.Price;
            var order = OrderViewModelBuilder.Build(model);
            var orderPartialViewModel = OrderPartialViewModelBuilder.Build(order, model.ClientUniqueId.Value,
                                                                           model.ProductSku.Value, clientFullName,
                                                                           productName, productPrice);
            return View(ConfirmViewModelBuilder.Build(orderPartialViewModel, model.ActionType));
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Edit(int orderId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var order = service.GetOrderById(orderId);
            var model = OrderViewModelBuilder.Build(order, ActionType.Edit, order.Product.Sku, order.Client.UniqueId);
            return View("Order", model);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AdministratorRoleName)]
        public ActionResult Edit(OrderViewModel model)
        {
            Validate(model);

            if (ModelState.IsValid)
            {
                var orderService = DependencyResolver.Current.Resolve<IOrderService>();
                var order = OrderViewModelBuilder.Build(model);
                
                orderService.EditOrder(order,model.ProductSku.Value,model.ClientUniqueId.Value,User.Identity.Name);
                return Redirect(Url.Action("Confirm", model));
            }
            return View("Order", model);
        }

        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult Delete(int orderId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            service.DeleteOrder(orderId);
            return Redirect(Url.Action("All"));
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult ChangeStatus(int orderId, OrderStatus status)
        {
            DependencyResolver.Current.Resolve<IOrderService>().ChangeStatus(orderId, status);
            return Redirect(Url.Action("My"));
        }
 
        

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
            var idExists = service.UniqueIdExists(parameter);
            return Json(new { result = idExists }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetClientByUniqueId(int uniqueId)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var client = service.GetClientByUniqueId(uniqueId);
            var model = ClientPartialViewModelBuilder.Build(new List<Client> { client }, 1);
            return PartialView("_Client", model);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetClientsByFullName(string firstName, string lastName, int position)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var clients = service.GetClientsByFullName(firstName, lastName).ToList();
            var model = ClientPartialViewModelBuilder.Build(clients, position);
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

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetOrderSummary(OrderViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            var orderService = DependencyResolver.Current.Resolve<IOrderService>();
            var client = orderService.GetClientByUniqueId(model.ClientUniqueId.Value);
            var clientFullName = client.FirstName + " " + client.LastName;
            var productService = DependencyResolver.Current.Resolve<IProductService>();
            var product = productService.GetProductBySku(model.ProductSku.Value);
            var productName = product.Name;
            var price = product.Price;
            var order = OrderViewModelBuilder.Build(model);
            var partialViewModel = OrderPartialViewModelBuilder.Build(order, model.ClientUniqueId.Value,
                                                                      model.ProductSku.Value, clientFullName,
                                                                      productName, price);

            return PartialView("_OrderSummary", partialViewModel);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult ValidateOrderModel(OrderViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Validate(model);
            var modelStateErrors =
                ModelState.Select(
                    modelState =>
                    new
                        {
                            elementId = modelState.Key,
                            message = String.Join("; ", modelState.Value.Errors.Select(error => error.ErrorMessage))
                        }).ToList();
            //return Json(new string[0], JsonRequestBehavior.AllowGet);
            return Json(modelStateErrors, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetMonthlyOrderAmountStatistics(string employeeLogin,int monthsAmount)
        {
            if (employeeLogin == null) throw new ArgumentNullException("employeeLogin");

            //TODO maybe use Dictionary<K,V> in service implementation instead of class?
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var afterDate = DateTime.Now.AddMonths(-monthsAmount);
            var orderStatistics = service.GetMonthlyOrderAmountStatistics(employeeLogin, afterDate).ToList();
            var result =
                new
                    {
                        dates = orderStatistics.Select(statistics => statistics.Date.ToShortDateString()),
                        amounts = orderStatistics.Select(statistics => statistics.OrderAmount)
                    };
                //orderStatistics.Select(os => new {date = os.Date.ToShortDateString(), amount = os.OrderAmount});

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult GetStatusesPercentageStatistics(string employeeLogin, int monthsAmount)
        {
            if (employeeLogin == null) throw new ArgumentNullException("employeeLogin");

            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var afterDate = DateTime.Now.AddMonths(-monthsAmount);
            var result = service.GetStatusesPercentageStatistics(employeeLogin, afterDate).OrderByDescending(pair=>pair.Value).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
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
            bool productIsAvailable = false;

            if (model.ClientUniqueId == null) ModelState.AddModelError("ClientUniqueId", "Client Id is requiered");
            else if (model.ClientUniqueId.Value < 0) ModelState.AddModelError("ClientUniqueId", "Client Id is non-negative");
            else if (!orderService.UniqueIdExists(model.ClientUniqueId.Value)) ModelState.AddModelError("ClientUniqueId", "There is no client with such id");

            if (model.ProductSku == null) ModelState.AddModelError("ProductSku", "Product SKU is requiered");
            else if (model.ProductSku.Value < 0) ModelState.AddModelError("ProductSku", "Product SKU is non-negative");
            else if (model.ActionType==ActionType.Create)
            {
                productIsAvailable = productService.ProductIsAvailable(model.ProductSku.Value);
                if (!productIsAvailable) ModelState.AddModelError("ProductSku", "This product is not available");
            }

            if (model.Amount == null) ModelState.AddModelError("Amount", "Amount is requiered");
            else if (model.Amount.Value <= 0) ModelState.AddModelError("Amount", "Amount is a positive number");
            else if (model.ProductSku != null && model.ActionType == ActionType.Create && productIsAvailable && !productService.ProductItemsAvailable(model.ProductSku.Value, model.Amount.Value)) ModelState.AddModelError("Amount", "There is no so much items available");
            else if(model.ProductSku!=null&&model.ActionType==ActionType.Edit&&(model.Amount.Value-model.OldAmount>0)&&!productService.ProductItemsAvailable(model.ProductSku.Value,model.Amount.Value-model.OldAmount)) ModelState.AddModelError("Amount","Can't add so much items, choose smaller amount");

            if (model.DeliveryDate == null) ModelState.AddModelError("DeliveryDate", "Delivery date is requiered");
            else if (model.ActionType==ActionType.Create&&model.DeliveryDate.Value < DateTime.Now) ModelState.AddModelError("DeliveryDate", "Choose delivery date after " + DateTime.Now.ToString(GlobalConstants.DateTimeFormat));

            if (model.DeliveryAddress == null) ModelState.AddModelError("DeliveryAddress", "Delivery address is requiered");
            else if (model.DeliveryAddress.Length > Order.MaxLengthFor.DeliveryAddress) ModelState.AddModelError("DeliveryAddress", "Delivery address is too long");

            if (model.ContactPhoneNumber == null) ModelState.AddModelError("ContactPhoneNumber", "Phone number is requiered");
            else if (model.ContactPhoneNumber.Length > Order.MaxLengthFor.ContactPhoneNumber) ModelState.AddModelError("ContactPhoneNumber", "Phone number is too long");
            else if (!PhoneNumberHelper.IsPhoneNumber(model.ContactPhoneNumber)) ModelState.AddModelError("ContactPhoneNumber", "Wrong format of phone number");




            //return ModelState.IsValid;
        }

        #endregion

    }
}
