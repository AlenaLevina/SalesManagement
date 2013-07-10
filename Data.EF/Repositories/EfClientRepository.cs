using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfClientRepository : EntityRepository<Client,int>,IClientRepository
    {
        public EfClientRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
