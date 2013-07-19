using System;
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
            if (String.IsNullOrEmpty(firstName) && String.IsNullOrEmpty(lastName)) return Context.Clients.ToList();
            if (!String.IsNullOrEmpty(firstName)  && !String.IsNullOrEmpty(lastName)) return Context.Clients.Where(c => c.FirstName.ToUpper().Contains(firstName.ToUpper()) && c.LastName.ToUpper().Contains(lastName.ToUpper()));
            if (!String.IsNullOrEmpty(firstName))
            {
                return Context.Clients.Where(c => c.FirstName.ToUpper().Contains(firstName.ToUpper()));
            }
            return Context.Clients.Where(c => c.LastName.ToUpper().Contains(lastName.ToUpper()));
        }
    }
}
