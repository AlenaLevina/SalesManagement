using Data.EF.Repositories;
using Data.Repositories;

namespace Data.EF
{
    public class EfUnitOfWork : UnitOfWork
    {
        private readonly SalesManagementContext _context;

        public SalesManagementContext Context
        {
            get { return _context; }
        }

        public EfUnitOfWork()
        {
            _context = new SalesManagementContext();
            
            AddRepository<ICategoryRepository>(() => new EfCategoryRepository(this));
            AddRepository<ICharacteristicRepository>(() => new EfCharacteristicRepository(this));
            AddRepository<ICharacteristicValueRepository>(() => new EfCharacteristicValueRepository(this));
            AddRepository<IClientRepository>(() => new EfClientRepository(this));
            AddRepository<IOrderRepository>(() => new EfOrderRepository(this));
            AddRepository<IProductRepository>(() => new EfProductRepository(this));
            AddRepository<IProfileRepository>(() => new EfProfileRepository(this));
            AddRepository<IRoleRepository>(() => new EfRoleRepository(this));
            AddRepository<IUserRepository>(() => new EfUserRepository(this));
        }

        public override void Commit()
        {
            Context.SaveChanges();
        }
    }
}
