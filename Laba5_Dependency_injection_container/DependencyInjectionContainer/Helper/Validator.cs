using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using DependencyInjectionContainer.Interfaces;

namespace DependencyInjectionContainer.Helper
{
    /// <summary>
    ///     Configuration validator
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        ///     Dependencies configuration
        /// </summary>
        private readonly IDependenciesConfiguration Configuration;

        /// <summary>
        ///     Nested types
        /// </summary>
        private readonly Stack<Type> NestedTypes;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// 
        /// <param name="configuration">Dependencies configuration</param>
        public Validator(IDependenciesConfiguration configuration)
        {
            Configuration = configuration;
            NestedTypes = new Stack<Type>();
        }

        /// <summary>
        ///     Check if configuration is valid
        /// </summary>
        /// 
        /// <returns>bool</returns>
        public bool IsValid()
        {
            return Configuration.Dependencies.Values.All(
                implementations => implementations.All(
                    implementation => CanBeCreated(implementation.ImplementationType)
                    )
                );
        }

        /// <summary>
        ///     Check that intance can be created
        /// </summary>
        /// 
        /// <param name="instanceType">Instance type</param>
        /// 
        /// <returns>bool</returns>
        private bool CanBeCreated(Type instanceType)
        {
            NestedTypes.Push(instanceType);
            
            var constructors = instanceType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                foreach (var parameter in parameters)
                {
                    Type parameterType;
                    
                    if (parameter.ParameterType.ContainsGenericParameters)
                    {
                        parameterType = parameter.ParameterType.GetInterfaces()[0];
                    }
                    else if (parameter.ParameterType.GetInterfaces().Any(i => i.Name == "IEnumerable"))
                    {
                        parameterType = parameter.ParameterType.GetGenericArguments()[0];
                    }
                    else
                    {
                        parameterType = parameter.ParameterType;
                    }

                    if (parameterType.IsInterface && Exists(parameterType))
                    {
                        continue;
                    }

                    NestedTypes.Pop();

                    return false;
                }
            }

            NestedTypes.Pop();

            return true;
        }

        /// <summary>
        ///     Check that necessary type exists in container
        /// </summary>
        /// 
        /// <param name="type">Type</param>
        /// 
        /// <returns>bool</returns>
        private bool Exists(Type type)
        {
            return Configuration.Dependencies.ContainsKey(type);
        }
    }
}
