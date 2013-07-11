using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IMembershipService
    {
        User CreateUser(string login, string password,string roleName);
        User GetUserByLogin(string login);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Role> GetAllRoles();
        void DeleteUserByLogin(string login);
        Role GetUserRole(string login);
        bool LoginExists(string login);
        void UpdateUserProfile(string login, Profile newProfile);
        Role CreateRole(string roleName);
        Profile GetUserProfile(string login);
        Role GetRoleByName(string roleName);
    }
}
