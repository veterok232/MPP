﻿using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Model;

namespace DependencyInjectionContainer.Block
{
    /// <summary>
    ///     Dependencies configuration interface
    /// </summary>
    public class DependenciesConfiguration : IDependenciesConfiguration
    {
        /// <summary>
        ///     Dependencies dictionary
        /// </summary>
        public Dictionary<Type, List<Implementation>> Dependencies { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DependenciesConfiguration()
        {
            Dependencies = new Dictionary<Type, List<Implementation>>();
        }

        /// <summary>
        ///     Register a new dependency
        /// </summary>
        /// 
        /// <typeparam name="TDependency">Dependency class</typeparam>
        /// <typeparam name="TImplementation">Implementation class</typeparam>
        /// 
        /// <param name="ttl">Time to live</param>
        /// <param name="number">Implementation number</param>
        public void Register<TDependency, TImplementation>(TTL ttl, ServiceImplementations number = ServiceImplementations.None) 
            where TDependency : class 
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), ttl, number);
        }

        /// <summary>
        ///     Register a new dependency
        /// </summary>
        /// 
        /// <param name="dependencyType">Dependency class</param>
        /// <param name="implementationType">Implementation class</param>
        /// <param name="ttl">Time to live</param>
        /// <param name="number">Implementation number</param>
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

        /// <summary>
        ///     Check that implementation and dependency are compatible
        /// </summary>
        /// 
        /// <param name="implementationType">Implementation class</param>
        /// <param name="dependencyType">Dependency class</param>
        /// 
        /// <returns>bool</returns>
        private bool IsCompatible(Type dependencyType, Type implementationType)
        {
            return implementationType.IsAssignableFrom(dependencyType) ||
                implementationType.GetInterfaces().Any(i => i.ToString() == dependencyType.ToString());
        }
    }
}
