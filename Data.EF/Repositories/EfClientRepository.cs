using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfClientRepository : EntityRepository<Client,int>,IClientRepository
    {
        public EfClientRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool UniqueIdExists(int id)
        {
            return Context.Clients.Any(c => c.UniqueId.Equals(id));
        }

        public Client GetByUniqueId(int id)
        {
            return Context.Clients.FirstOrDefault(e => e.UniqueId.Equals(id));
        }

        public IEnumerable<Client> GetByFullName(string firstName, string lastName)
        {
            if (firstName == null && lastName == null) return Context.Clients.ToList();
            if (firstName != null && lastName != null) return Context.Clients.Where(c => c.FirstName.Contains(firstName) && c.LastName.Contains(lastName));
            if (firstName != null)
            {
                return Context.Clients.Where(c => c.FirstName.Contains(firstName));
            }
            return Context.Clients.Where(c => c.LastName.Contains(lastName));
        }
    }
}
