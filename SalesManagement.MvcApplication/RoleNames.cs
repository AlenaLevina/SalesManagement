namespace SalesManagement.MvcApplication
{
    public static class RoleNames
    {
        public const string EmployeeRoleName = "Employee";
        public const string ManagerRoleName = "Manager";
        public const string AdministratorRoleName = "Administrator";
        public static readonly string[] AllRoles = new []{EmployeeRoleName,ManagerRoleName,AdministratorRoleName};
        public const string AllRoleNames = EmployeeRoleName + "," + ManagerRoleName + "," +
                                                     AdministratorRoleName;
#if DEBUG
        public const string ManagerActionsRoleName = ManagerRoleName + "," + AdministratorRoleName;
        public const string EmployeeActionsRoleName = EmployeeRoleName + "," + AdministratorRoleName;
#else
        public const string ManagerActionsRoleName = ManagerRoleName;
        public const string EmployeeActionsRoleName = EmployeeRoleName;
#endif

    }
}