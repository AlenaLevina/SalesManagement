using System;
using System.Collections.Generic;
using Data.Exceptions;


namespace Data.Repositories
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected void AddRepository<TRepository>(Func<TRepository> repositoryConstructor)
        {
            if (repositoryConstructor == null) throw new ArgumentNullException("repositoryConstructor");

            var repositoryType = typeof(TRepository);
            if (!_repositories.ContainsKey(repositoryType))
            {
                _repositories.Add(repositoryType, repositoryConstructor());
            }
        }

        public abstract void Commit();

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var repositoryType = typeof(TRepository);
            if (_repositories.ContainsKey(repositoryType))
            {
                return (TRepository)_repositories[repositoryType];
            }
            throw new DataException("Repository not found");
        }
    }
}
