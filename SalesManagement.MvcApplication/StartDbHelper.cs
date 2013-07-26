using Common.Dependency;
using Common.Helpers;
using Contracts;

namespace SalesManagement.MvcApplication
{
    public class StartDbHelper
    {
        public static void CreateRoles()
        {
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            foreach (var role in RoleNames.AllRoles)
            {
                var existingRole = service.GetRoleByName(role);
                if (existingRole == null)
                {
                    service.CreateRole(role);
                }
            }
        }

        public static void CreateAdmin()
        {
            const string adminLogin = "Admin";
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            if (!service.LoginExists(adminLogin))
            {
                service.CreateUser(adminLogin, "123", RoleNames.AdministratorRoleName);
            }
        }

        public static void Prepare()
        {
            CreateRoles();
            CreateAdmin();
        }
    }
}