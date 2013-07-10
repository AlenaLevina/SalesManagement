using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfOrderRepository : EntityRepository<Order, int>, IOrderRepository
    {
        public EfOrderRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
