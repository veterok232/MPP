using DependencyInjectionContainer.Model;
using System;

namespace DependencyInjectionContainer.Interfaces
{
    public interface IDependencyProvider
    {
        TDependency Resolve<TDependency>(ServiceImplementations number = ServiceImplementations.Any)
            where TDependency : class;

        object Resolve(Type dependencyType, ServiceImplementations number = ServiceImplementations.Any);
    }
}
