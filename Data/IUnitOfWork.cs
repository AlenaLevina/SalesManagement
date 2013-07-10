using Data.Repositories;

namespace Data
{
    public interface IUnitOfWork
    {
        void Commit();
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
