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
            var service = DependencyResolver.Current.Resolve<IMembershipService>();
            service.CreateUser("Admin", "123", RoleNames.AdministratorRoleName);
        }

        public static void Prepare()
        {
            CreateRoles();
            CreateAdmin();
        }
    }
}