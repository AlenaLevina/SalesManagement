using System;
using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfUserRepository : EntityRepository<User,int>, IUserRepository
    {
        public EfUserRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetByLogin(string login)
        {
            if (login == null) throw new ArgumentNullException("login");

            return Context.Users.FirstOrDefault(e => e.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public bool LoginExists(string login)
        {
            if (login == null) throw new ArgumentNullException("login");

            return Context.Users.Any(e => e.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }
    }
}
