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

        public bool ClientIdExists(int id)
        {
            return Context.Clients.Any(c => c.ClientId.Equals(id));
        }

        public Client GetByClientId(int id)
        {
            return Context.Clients.FirstOrDefault(e => e.ClientId.Equals(id));
        }
    }
}
