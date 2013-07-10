using Model;

namespace Data.Repositories
{
    public interface IRoleRepository : IRepository<Role,int>
    {
        Role GetByName(string name);
    }
}
