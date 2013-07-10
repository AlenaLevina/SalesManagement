namespace Common.Dependency
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
}
