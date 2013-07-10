using System;
using System.Linq;
using Data.Exceptions;
using Data.Repositories;
using Model;

namespace Data.EF.Repositories
{
    public class EfCategoryRepository:EntityRepository<Category,int>,ICategoryRepository
    {
        public EfCategoryRepository(EfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetByName(string name)
        {
            try
            {
                return Context.Categories.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new DataException(DbErrorMessage, e);
            }
        }
    }
}
