using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfProductRepository : EntityRepository<Product, int>, IProductRepository
    {
        public EfProductRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
