using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Helper;
using DependencyInjectionContainer.Model;

namespace DependencyInjectionContainer.Block
{
    public class DependencyProvider : IDependencyProvider
    {
        private readonly IDependenciesConfiguration Configuration;

        private readonly Dictionary<Type, List<Singleton>> Singletons;

        public DependencyProvider(IDependenciesConfiguration configuration)
        {
            IValidator configValidator = new Validator(configuration);
            if (!configValidator.IsValid())
            {
                throw new ArgumentException("Invalid configuration!");
            }

            Configuration = configuration;
            Singletons = new Dictionary<Type, List<Singleton>>();
        }

        public TDependency Resolve<TDependency>(ServiceImplementations number = ServiceImplementations.Any)
            where TDependency : class
        {
            return (TDependency)Resolve(typeof(TDependency), number);
        }

        public object Resolve(Type dependencyType, ServiceImplementations number = ServiceImplementations.Any)
        {
            object result;

            if (IsEnumerable(dependencyType))
            {
                result = CreateEnumerable(dependencyType.GetGenericArguments()[0]);
            }
            else
            {
                Implementation container = GetContainerByDependencyType(dependencyType, number);
                Type type = GetGeneratedType(dependencyType, container.ImplementationType);

                result = ResolveNonEnumerable(type, container.TimeToLive, dependencyType, container.Number);
            }

            return result;
        }

        private bool IsEnumerable(Type dependencyType)
        {
            return dependencyType.GetInterfaces().Any(i => i.Name == "IEnumerable");
        }

        private IList CreateEnumerable(Type dependencyType)
        {
            var result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(dependencyType));
            var containers = Configuration.Dependencies[dependencyType];

            foreach (var container in containers)
            {
                var instance = ResolveNonEnumerable(container.ImplementationType, container.TimeToLive, dependencyType, container.Number);
                result.Add(instance);
            }

            return result;
        }

        private Implementation GetContainerByDependencyType(Type dependencyType, ServiceImplementations number)
        {
            Implementation container;

            if (dependencyType.IsGenericType)
            {
                container = GetLastContainer(dependencyType, number);
                container ??= GetLastContainer(dependencyType.GetGenericTypeDefinition(), number);
            }
            else
            {
                container = GetLastContainer(dependencyType, number);
            }

            return container;
        }

        private Type GetGeneratedType(Type dependencyType, Type implementationType)
        {
            if (dependencyType.IsGenericType && implementationType.IsGenericTypeDefinition)
            {
                return implementationType.MakeGenericType(dependencyType.GetGenericArguments());
            }

            return implementationType;
        }

        private object ResolveNonEnumerable(Type implementationType, TTL ttl, Type dependencyType, ServiceImplementations number)
        {
            if (ttl != TTL.Singleton)
            {
                return CreateInstance(implementationType);
            }

            lock (Configuration)
            {
                if (IsInSingletons(dependencyType, implementationType, number))
                {
                    return Singletons[dependencyType].Find(singletonContainer => number.HasFlag(singletonContainer.Number)).Instance;
                }

                var result = CreateInstance(implementationType);
                AddToSingletons(dependencyType, result, number);

                return result;
            }
        }

        private Implementation GetLastContainer(Type dependencyType, ServiceImplementations number)
        {
            if (Configuration.Dependencies.ContainsKey(dependencyType))
            {
                return Configuration.Dependencies[dependencyType].FindLast(container => number.HasFlag(container.Number));
            }

            return null;
        }

        private bool IsInSingletons(Type dependencyType, Type implementationType, ServiceImplementations number)
        {
            var list = Singletons.ContainsKey(dependencyType) ? Singletons[dependencyType] : null;

            return list?.Find(container => number.HasFlag(container.Number) && container.Instance.GetType() == implementationType) != null;
        }

        private void AddToSingletons(Type dependencyType, object implementation, ServiceImplementations number)
        {
            if (Singletons.ContainsKey(dependencyType))
            {
                Singletons[dependencyType].Add(new Singleton(implementation, number));
            }
            else
            {
                Singletons.Add(dependencyType, new List<Singleton>(){ new Singleton(implementation, number) });
            }
        }

        private object CreateInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            foreach (var constructor in constructors)
            {
                var generatedParams = new List<dynamic>();

                var constructorParams = constructor.GetParameters();
                foreach (var parameterInfo in constructorParams)
                {
                    dynamic parameter;
                    if (parameterInfo.ParameterType.IsInterface)
                    {
                        var number = parameterInfo.GetCustomAttribute<DependencyKeyAttribute>()?.Number ?? ServiceImplementations.Any;
                        parameter = Resolve(parameterInfo.ParameterType, number);
                    }
                    else
                    {
                        break;
                    }

                    generatedParams.Add(parameter);
                }

                return constructor.Invoke(generatedParams.ToArray());
            }

            throw new ArgumentException("Can't create class instance!");
        }        
    }
}
