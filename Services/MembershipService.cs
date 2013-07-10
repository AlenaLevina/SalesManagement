using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Helpers;
using Contracts;
using Data.Exceptions;
using Data.Repositories;
using Model;

namespace Services
{
    public class MembershipService:BaseService,IMembershipService
    {
        public User CreateUser(string login, string password, string roleName)
        {
            if (login == null) throw new ArgumentNullException("login");
            if (password == null) throw new ArgumentNullException("password");
            if (roleName == null) throw new ArgumentNullException("roleName");

            var generatedSalt = PasswordHelper.GenerateSalt(User.MaxLengthFor.PasswordSalt);
            var userRole = GetRoleByName(roleName);
            if (userRole==null) throw new DataException("Role not found");
            var user = new User
            {
                Login = login,
                PasswordHash = PasswordHelper.ComputeHash(password, generatedSalt),
                PasswordSalt = generatedSalt,
                //Id = Guid.NewGuid(), //TODO автогенерация id??
                Role = userRole,
                Profile = new Profile()
            };
            GetRepository<IUserRepository>().Create(user);
            return user;
        }

        //public Profile CreateProfile(Profile profile)
        //{
        //    if (profile == null) throw new ArgumentNullException("profile");
        //    GetRepository<IProfileRepository>().Create(profile);
        //    return GetRepository<IProfileRepository>().
        //}

        public Role GetUserRole(string login)
        {
            var user = GetRepository<IUserRepository>().GetByLogin(login);
            if (user != null)
            {
                return user.Role;
            }
            return null;
        }
        
        public User GetUserByLogin(string login)
        {
            if (login == null) throw new ArgumentNullException("login");
            return GetRepository<IUserRepository>().GetByLogin(login);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return GetRepository<IUserRepository>().GetAll();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return GetRepository<IRoleRepository>().GetAll();
        }

        public void DeleteUserByLogin(string login)
        {
            if (login == null) throw new ArgumentNullException("login");
            var user = GetUserByLogin(login);
            if (user != null)
            {
                GetRepository<IUserRepository>().Delete(user.Id);
            }
        }

        public bool LoginExists(string login)
        {
            if (login == null) throw new ArgumentNullException("login");

            return GetRepository<IUserRepository>().LoginExists(login);
        }

        public void UpdateUserProfile(string login, Profile newProfile)
        {
            if (login == null) throw new ArgumentNullException("login");
            if (newProfile == null) throw new ArgumentNullException("newProfile");


            var repository = GetRepository<IProfileRepository>();
            var profile = repository.GetByUserLogin(login);//user.Profile);
            if (profile != null)
            {
                profile.CopyFrom(newProfile);
                repository.Update(profile);
            }
        }

        public Role CreateRole(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");
            var role = new Role { Name = roleName };
            GetRepository<IRoleRepository>().Create(role);
            return role;
        }

        public Profile GetUserProfile(string login)
        {
            if (login == null) throw new ArgumentNullException("login");

            Profile profile = null;
            var user = GetRepository<IUserRepository>().GetByLogin(login);
            if (user != null)
            {
                if (user.ProfileId != null) profile = GetRepository<IProfileRepository>().Get(user.ProfileId.Value);
            }
            return profile;
        }

        public Role GetRoleByName(string roleName)
        {
            if (roleName == null) throw new ArgumentNullException("roleName");
            var role = GetRepository<IRoleRepository>().GetByName(roleName);
            return role;
        }

        public Profile GetUserProfile(int userId)
        {
            Profile profile = null;
            var user = GetRepository<IUserRepository>().Get(userId);
            if (user != null)
            {
                if (user.ProfileId != null) profile = GetRepository<IProfileRepository>().Get(user.ProfileId.Value);
            }
            return profile;
        }
    }
}
