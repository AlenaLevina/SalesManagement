using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Data.Exceptions;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public abstract class EntityRepository<TEntity,TKey> : Repository,IRepository<TEntity,TKey> where TEntity : Entity<TKey>
    {
        protected const string DbErrorMessage = "Problem with database. See inner exception for details.";

        private readonly SalesManagementContext _context;

        protected SalesManagementContext Context
        {
            get { return _context; }
        }

        protected EntityRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _context = unitOfWork.Context;
        }

        public virtual void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            ValidateEntity(entity);
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public TEntity Get(TKey key) 
        {
            try
            {
                return _context.Set<TEntity>().Find(key);
            }
            catch (Exception ex)
            {
                throw new DataException(DbErrorMessage, ex);
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                throw new DataException(DbErrorMessage, ex);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            
            ValidateEntity(entity);
            var contextEntry = _context.Entry(entity);
            if (contextEntry != null)
            {
                var oldEntity = _context.Set<TEntity>().Find(entity.Id);
                if (oldEntity != null)
                {
                    var contextOldEntry = _context.Entry(oldEntity);
                    contextOldEntry.CurrentValues.SetValues(entity);
                    contextOldEntry.State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(TKey key)
        {
            try
            {
                var entity = _context.Set<TEntity>().Find(key);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DataException(DbErrorMessage, ex);
            }
        }

        protected void ValidateEntity(object entity)
        {
            var contextEntry = _context.Entry(entity);
            if (contextEntry != null)
            {
                var validationResult = contextEntry.GetValidationResult();
                if (!validationResult.IsValid)
                {
                    throw new DataException("Provided data does not conform storage schema");
                }
            }
        }
    }


}
