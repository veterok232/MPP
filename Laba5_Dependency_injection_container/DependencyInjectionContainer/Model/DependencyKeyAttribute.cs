using System;

namespace DependencyInjectionContainer.Model
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DependencyKeyAttribute : Attribute
    {
        public ServiceImplementations Number { get; }

        public DependencyKeyAttribute(ServiceImplementations number)
        {
            Number = number;
        }
    }
}
