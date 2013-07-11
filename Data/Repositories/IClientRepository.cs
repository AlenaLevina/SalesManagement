using Model;

namespace Data.Repositories
{
    public interface IClientRepository:IRepository<Client,int>
    {
        bool ClientIdExists(int id);
    }
}
