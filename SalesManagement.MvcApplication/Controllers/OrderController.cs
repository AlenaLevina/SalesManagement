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
#if DEBUG
        public ActionResult Index()
        {
            return Json("Hello, DEBUG is defined",JsonRequestBehavior.AllowGet);
        }
#endif
        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult GenerateId()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var newId = service.GetNewId(GlobalConstants.ClientIdLength);
            return Json(newId, JsonRequestBehavior.AllowGet);
        }

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
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            return View("Client", ClientViewModelBuilder.Build(new Client(), ActionType.Create));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ManagerActionsRoleName)]
        public ActionResult CreateClient(ClientViewModel model)
        {
            #region Validation

            //var service = DependencyResolver.Current.Resolve<IOrderService>();
            //if (model.ClientId == null) ModelState.AddModelError("ClientId", "Please, generate client id");
            //else if (service.ClientIdExists(model.ClientId.Value))
            //    ModelState.AddModelError("ClientId", "Such client id already exists");
            //if (model.FirstName == null) ModelState.AddModelError("FirstName", "First name is requiered");
            //else if (model.FirstName.Length > Client.MaxLengthFor.FirstName)
            //    ModelState.AddModelError("FirstName", "First name is too long");
            //if (model.LastName == null) ModelState.AddModelError("LastName", "Last name is requiered");
            //else if (model.LastName.Length > Client.MaxLengthFor.LastName)
            //    ModelState.AddModelError("LastName", "Last name is too long");
            //if (model.Phone == null) ModelState.AddModelError("Phone", "Phone number is requiered");
            //else if (!PhoneNumberHelper.IsPhoneNumber(model.Phone)) ModelState.AddModelError("Phone", "Phone number can contain only digits and hyphens \"-\"");
            //else if (model.Phone.Length > Client.MaxLengthFor.Phone)
            //    ModelState.AddModelError("Phone", "Phone number is too long");
            //if (model.Address == null) ModelState.AddModelError("Address", "Address is requiered");
            //else if (model.Address.Length > Client.MaxLengthFor.Address)
            //    ModelState.AddModelError("Address", "Address is too long");
            //if (model.Email == null) ModelState.AddModelError("Email", "Email is requiered");
            //else
            //{
            //    if (model.Email.Length > Client.MaxLengthFor.Email)
            //        ModelState.AddModelError("Email", "Email is too long");
            //    var regex =
            //        new Regex(
            //            @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$");
            //    if (!regex.IsMatch(model.Email)) ModelState.AddModelError("Email", "Wrong e-mail format");
            //}

            #endregion

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
            var client = service.GetClientByClientId(id);
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

        private void Validate(ClientViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            if (model.ClientId == null) ModelState.AddModelError("ClientId", "Please, generate client id");
            else if (model.ActionType == ActionType.Create && service.ClientIdExists(model.ClientId.Value))
                ModelState.AddModelError("ClientId", "Such client id already exists");
            if (model.FirstName == null) ModelState.AddModelError("FirstName", "First name is requiered");
            else if (model.FirstName.Length > Client.MaxLengthFor.FirstName)
                ModelState.AddModelError("FirstName", "First name is too long");
            if (model.LastName == null) ModelState.AddModelError("LastName", "Last name is requiered");
            else if (model.LastName.Length > Client.MaxLengthFor.LastName)
                ModelState.AddModelError("LastName", "Last name is too long");
            if (model.Phone == null) ModelState.AddModelError("Phone", "Phone number is requiered");
            else if (!PhoneNumberHelper.IsPhoneNumber(model.Phone)) ModelState.AddModelError("Phone", "Phone number can contain only digits and hyphens \"-\"");
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
    }
}
