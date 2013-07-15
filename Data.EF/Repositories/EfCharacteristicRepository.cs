using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfCharacteristicRepository:EntityRepository<Characteristic,int>,ICharacteristicRepository
    {
        public EfCharacteristicRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Characteristic> GetByCategoryId(int categoryId)
        {
            return
                Context.Characteristics.Where(
                    characteristic => characteristic.Categories.Any(category => category.Id.Equals(categoryId)));
        }
    }
}
