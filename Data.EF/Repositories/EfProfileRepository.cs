using System;
using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfProfileRepository : EntityRepository<Profile, int>, IProfileRepository
    {
        public EfProfileRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Profile GetByUserLogin(string login)
        {
            Profile profile=null;
            var user = Context.Users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                profile = user.Profile;
            }
            return profile;
        }
    }
}
