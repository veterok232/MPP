using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Model;

namespace DependencyInjectionContainer.Interfaces
{ 
    public interface IDependenciesConfiguration
    {
        public Dictionary<Type, List<Implementation>> Dependencies { get; }

        void Register<TDependency, TImplementation>(TTL ttl, ServiceImplementations number = ServiceImplementations.None)
            where TDependency : class
            where TImplementation : TDependency;

        void Register(Type dependencyType, Type implementationType, TTL ttl, ServiceImplementations number = ServiceImplementations.None);
    }
}
