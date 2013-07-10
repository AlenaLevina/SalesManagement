using Model;

namespace Data.Repositories
{
    public interface ICategoryRepository:IRepository<Category,int>
    {
        Category GetByName(string name);
    }
}
