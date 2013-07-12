using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfProductRepository : EntityRepository<Product, int>, IProductRepository
    {
        public EfProductRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool SkuExists(int sku)
        {
            return Context.Products.Any(c => c.Sku.Equals(sku));
        }
    }
}
