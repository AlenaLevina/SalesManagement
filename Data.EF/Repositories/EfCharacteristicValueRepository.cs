using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfCharacteristicValueRepository:EntityRepository<CharacteristicValue,int>,ICharacteristicValueRepository
    {
        public EfCharacteristicValueRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<CharacteristicValue> GetByProductId(int productId)
        {
            return Context.CharacteristicValues.Where(e => e.ProductId.Equals(productId));
        }
    }
}
