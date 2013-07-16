using Model;

namespace Data.Repositories
{
    public interface IClientRepository:IRepository<Client,int>
    {
        bool UniqueIdExists(int id);
        Client GetByUniqueId(int id);
    }
}
