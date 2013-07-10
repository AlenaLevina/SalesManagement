using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity, in TKey> : IRepository where TEntity : Entity<TKey>
    {
        void Create(TEntity entity);
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}