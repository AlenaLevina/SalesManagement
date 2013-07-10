using Model;

namespace Data.Repositories
{
    public interface IUserRepository:IRepository<User,int>
    {
        User GetByLogin(string login);
        bool LoginExists(string login);
    }
}
