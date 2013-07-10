using Common.Dependency;
using Data;
using Data.Repositories;

namespace Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService()
        {
            UnitOfWork = DependencyResolver.Current.Resolve<IUnitOfWork>();
        }

        protected static TService GetService<TService>() where TService : BaseService
        {
            return DependencyResolver.Current.Resolve<TService>();
        }

        protected TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return UnitOfWork.GetRepository<TRepository>();
        }
    }
}
