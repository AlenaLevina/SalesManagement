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
    }
}
