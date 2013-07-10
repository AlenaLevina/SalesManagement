using System;
using System.Linq;
using Data.Exceptions;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfRoleRepository : EntityRepository<Role, int>, IRoleRepository
    {
        public EfRoleRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Role GetByName(string name)
        {
            try
            {
                return Context.Roles.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new DataException(DbErrorMessage,e);
            }
        }
    }
}
