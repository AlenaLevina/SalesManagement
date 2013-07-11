using System.Text.RegularExpressions;
using System.Web.Mvc;
using Common.Helpers;
using Contracts;
using Model;
using SalesManagement.MvcApplication.ViewModelBuilders.Order;
using SalesManagement.MvcApplication.ViewModels.Order;
using DependencyResolver = Common.Dependency.DependencyResolver;

namespace SalesManagement.MvcApplication.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerRoleName)]
        public ActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ManagerRoleName)]
        public ActionResult CreateClient(CreateClientViewModel model)
        {
            #region Validation

            var service = DependencyResolver.Current.Resolve<IOrderService>();
            if (model.ClientId == null) ModelState.AddModelError("ClientId", "Please, generate client id");
            else if (service.ClientIdExists(model.ClientId.Value))
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

            #endregion
            
            if (ModelState.IsValid)
            {
                service.CreateClient(new Client
                    {
                        Address = model.Address,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        ClientId = model.ClientId.Value,
                        LastName = model.LastName,
                        Phone = model.Phone
                    });
                model.Success = true;
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.ManagerRoleName)]
        public ActionResult GenerateId()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var newId = service.GetNewId(GlobalConstants.ClientIdLength);
            return Json(newId,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clients()
        {
            var service = DependencyResolver.Current.Resolve<IOrderService>();
            var clients = service.GetAllClients();
            return View(ClientsViewModelBuilder.Build(clients));
        }

    }
}
