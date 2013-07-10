using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfCharacteristicRepository:EntityRepository<Characteristic,int>,ICharacteristicRepository
    {
        public EfCharacteristicRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
