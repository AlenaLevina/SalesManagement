using Model;

namespace Data.Repositories
{
    public interface IProductRepository:IRepository<Product,int>
    {
        bool SkuExists(int sku);
    }
}
