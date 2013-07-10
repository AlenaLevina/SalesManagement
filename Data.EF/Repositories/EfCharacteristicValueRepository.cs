using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfCharacteristicValueRepository:EntityRepository<CharacteristicValue,int>,ICharacteristicValueRepository
    {
        public EfCharacteristicValueRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
