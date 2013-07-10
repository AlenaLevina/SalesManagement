using System.Collections.Generic;
using Contracts;
using Data.Repositories;
using Model;

namespace Services
{
    public class ProductService:BaseService,IProductService
    {
        public void CreateCategory(string name, IEnumerable<Characteristic> characteristics)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Characteristic> GetAllCharacteristics()
        {
            return GetRepository<ICharacteristicRepository>().GetAll();
        }
    }
}
