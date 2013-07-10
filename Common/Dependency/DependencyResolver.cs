using System;

namespace Common.Dependency
{
    public class DependencyResolver
    {
        public static IDependencyResolver Current { get; private set; }

        public static void SetResolver(IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");

            Current = dependencyResolver;l
        }

    }
}
