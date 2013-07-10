using Autofac;
using Common.Dependency;
using Contracts;
using Data;
using Data.EF;
using Services;

namespace SalesManagement.MvcApplication
{
    public class SalesManagementDependencyResolver: Common.Dependency.IDependencyResolver
    {
        private readonly IContainer _container;

        public SalesManagementDependencyResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<MembershipService>().As<IMembershipService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            _container = builder.Build();
        }
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}