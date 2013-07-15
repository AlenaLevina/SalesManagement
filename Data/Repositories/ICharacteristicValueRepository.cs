using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface ICharacteristicValueRepository:IRepository<CharacteristicValue,int>
    {
        IEnumerable<CharacteristicValue> GetByProductId(int productId);
    }
}
