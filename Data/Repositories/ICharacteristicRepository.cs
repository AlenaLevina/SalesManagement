using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface ICharacteristicRepository:IRepository<Characteristic,int>
    {
        IEnumerable<Characteristic> GetByCategoryId(int categoryId);
    }
}
