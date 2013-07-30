using System.Collections.Generic;
using Data.Repositories;
using System.Linq;
using Model;

namespace Data.EF.Repositories
{
    public class EfOrderRepository : EntityRepository<Order, int>, IOrderRepository
    {
        public EfOrderRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Order> GetAllByEmployeeId(int id)
        {
            return Context.Orders.Where(e => e.EmployeeId.Equals(id));
        }
    }
}
