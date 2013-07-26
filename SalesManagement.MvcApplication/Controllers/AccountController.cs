using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model;
using SalesManagement.MvcApplication.ViewModelBuilders.Account;
using DependencyResolver = Common.Dependency.DependencyResolver;
using Contracts;
using SalesManagement.MvcApplication.ViewModels.Account;

namespace SalesManagement.MvcApplication.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [Authorize(Roles =RoleNames.ManagerRoleName+","+RoleNames.AdministratorRoleName)]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ManagerRoleName + "," + RoleNames.AdministratorRoleName)]
        public ActionResult Register(RegisterViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            if (model.Login == null) ModelState.AddModelError("Login", "Login is requiered");
            else
            {
                if (model.Login.Length > Model.User.MaxLengthFor.Login) ModelState.AddModelError("Login", "Login is too long");
                else if (service.LoginExists(model.Login)) ModelState.AddModelError("Login", "User with such login already exists");
            }
            if (model.Password == null) ModelState.AddModelError("Password", "Password is requiered");
            else if (model.RepeatedPassword == null) ModelState.AddModelError("RepeatedPassword", "Repeat password");
            else if (!model.Password.Equals(model.RepeatedPassword, StringComparison.Ordinal)) ModelState.AddModelError("RepeatedPassword", "Passwords don't match");
            if(model.Role==null) ModelState.AddModelError("Role","Role is requiered");

            model.SuccessfullyRegistered = false;
            if (ModelState.IsValid)
            {
                if (User.IsInRole(RoleNames.ManagerRoleName) && (!model.Role.Equals(RoleNames.EmployeeRoleName)))
                {
                    return Redirect(Url.Action("SignIn"));
                }
                service.CreateUser(model.Login, model.Password, model.Role);
                model.SuccessfullyRegistered = true;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel model)
        {
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            User user = null;
            if (model.Login == null) ModelState.AddModelError("Login", "Enter login");
            else
            {
                user = service.GetUserByLogin(model.Login);
                if (user == null) ModelState.AddModelError("Login", "There is no user with such login");
            }
            if (model.Password == null) ModelState.AddModelError("Password", "Enter password");
            else if (user != null && !user.VerifyPassword(model.Password)) ModelState.AddModelError("Password", "Wrong password");

            if (ModelState.IsValid)
            {
                var ticket = new FormsAuthenticationTicket(1, user.Login, DateTime.UtcNow,
                                                           DateTime.UtcNow + new TimeSpan(30, 0, 0, 0), true,
                                                           user.Login);
                var et = FormsAuthentication.Encrypt(ticket);
                HttpContext.Response.Cookies.Add(new HttpCookie(GlobalConstants.AuthenticationCookieName, et));
                if (Request.Params["ReturnUrl"]!=null){return Redirect(Request.Params["ReturnUrl"]);}
                return Redirect(Url.Action("ViewProfile", "Account"));
            }
            return View();
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Index", "Home"));
        }

        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult ViewProfile(string login)
        {
            if (login == null) login = User.Identity.Name;
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            return View(ViewProfileViewModelBuilder.Build(service.GetUserProfile(login), service.GetUserRole(login).Name, login));
        }
        
        [HttpGet]
        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult EditProfile()
        {
            var login = User.Identity.Name;
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            var profile = service.GetUserProfile(login);
            return View(EditProfileViewModelBuilder.Build(profile));
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.AllRoleNames)]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (model.FirstName != null && model.FirstName.Length > Model.Profile.MaxLengthFor.FirstName) ModelState.AddModelError("FirstName", "First name is too long");
            if (model.LastName != null && model.LastName.Length > Model.Profile.MaxLengthFor.LastName) ModelState.AddModelError("LastName", "Last name is too long");
            if (model.DateOfBirth!=null&&model.DateOfBirth.Value>=DateTime.Now) ModelState.AddModelError("DateOfBirth","Date of birth must be before today");
            
            if (ModelState.IsValid)
            {
                var service = DependencyResolver.Current.Resolve<IMembershipService>();
                var login = User.Identity.Name;
                string fileName = model.ImageUrl;
                if (model.Image != null)
                {
                    var extension = model.Image.FileName.Substring(model.Image.FileName.LastIndexOf('.'));
                    fileName = login + extension;
                    var projectDirectory = Directory.GetParent((string)AppDomain.CurrentDomain.GetData("DataDirectory"));
                    model.Image.SaveAs(projectDirectory + GlobalConstants.UploadUserImageDirectory + fileName);
                }
                service.UpdateUserProfile(login, EditProfileViewModelBuilder.Build(model, fileName));
                return Redirect(Url.Action("ViewProfile"));
            }
            return View(model);

        }
    }
}
