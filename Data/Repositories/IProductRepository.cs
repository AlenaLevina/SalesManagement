using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface IProductRepository:IRepository<Product,int>
    {
        bool SkuExists(int sku);
        int GetIdBySku(int sku);
        Product GetBySku(int sku);
        IEnumerable<Product> GetByName(string name);
    }
}
