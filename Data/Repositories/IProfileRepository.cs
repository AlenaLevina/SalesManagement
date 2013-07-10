using Model;

namespace Data.Repositories
{
    public interface IProfileRepository:IRepository<Profile,int>
    {
        Profile GetByUserLogin(string login);
    }
}
