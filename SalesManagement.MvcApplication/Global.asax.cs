using System;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using Contracts;
using Data.EF.Migrations;
using DependencyResolver = Common.Dependency.DependencyResolver;
namespace SalesManagement.MvcApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MigrationHelper.Prepare();

            DependencyResolver.SetResolver(new SalesManagementDependencyResolver());
            StartDbHelper.Prepare();
            Mapper.Initialize(config=>config.AddProfile<SalesManagementProfile>());
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[GlobalConstants.AuthenticationCookieName];
            if (authCookie != null && (authCookie.Value != string.Empty))
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null)
                {
                    var login = ticket.Name;
                    if (login != null)
                    {
                        var roles = new string[]
                            {DependencyResolver.Current.Resolve<IMembershipService>().GetUserRole(login).Name};
                        var identity = new FormsIdentity(ticket);
                        Context.User = new GenericPrincipal(identity, roles);
                    }
                }
            }
        }
    }
}