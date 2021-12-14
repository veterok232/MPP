using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Model;

namespace DependencyInjectionContainer.Block
{
    public class DependenciesConfiguration : IDependenciesConfiguration
    {
        public Dictionary<Type, List<Implementation>> Dependencies { get; private set; }

        public DependenciesConfiguration()
        {
            Dependencies = new Dictionary<Type, List<Implementation>>();
        }

        public void Register<TDependency, TImplementation>(TTL ttl, ServiceImplementations number = ServiceImplementations.None) 
            where TDependency : class 
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), ttl, number);
        }

        public void Register(Type dependencyType, Type implementationType, TTL ttl, ServiceImplementations number = ServiceImplementations.None)
        {
            if (!IsCompatible(dependencyType, implementationType))
            {
                throw new ArgumentException("Incompatible types!");
            }

            var container = new Implementation(implementationType, ttl, number);

            if (Dependencies.ContainsKey(dependencyType))
            {
                var index = Dependencies[dependencyType].FindIndex(elem => elem.ImplementationType == container.ImplementationType);
                
                if (index != -1)
                {
                    Dependencies[dependencyType].RemoveAt(index);
                }

                Dependencies[dependencyType].Add(container);

            }
            else
            {
                Dependencies.Add(dependencyType, new List<Implementation>() { container });
            }
        }

        private bool IsCompatible(Type dependencyType, Type implementationType)
        {
            return implementationType.IsAssignableFrom(dependencyType) ||
                implementationType.GetInterfaces().Any(i => i.ToString() == dependencyType.ToString());
        }
    }
}
