using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface IClientRepository:IRepository<Client,int>
    {
        bool UniqueIdExists(int id);
        Client GetByUniqueId(int id);
        IEnumerable<Client> GetByFullName(string firstName, string lastName);
    }
}
