using System;

namespace Data.Repositories
{
    public abstract class Repository
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        protected Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            UnitOfWork = unitOfWork;
        }
    }
}
