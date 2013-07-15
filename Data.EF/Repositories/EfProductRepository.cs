using System.Linq;
using Data.Exceptions;
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

        public int GetIdBySku(int sku)
        {
            var product = Context.Products.FirstOrDefault(e => e.Sku.Equals(sku));
            if (product == null) throw new DataException("No product with such sku");
            return product.Id;
        }

        public Product GetBySku(int sku)
        {
            return Context.Products.FirstOrDefault(e => e.Sku.Equals(sku));
        }
    }
}
