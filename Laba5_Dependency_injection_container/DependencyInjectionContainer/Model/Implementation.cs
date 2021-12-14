using System;

namespace DependencyInjectionContainer.Model
{
    public class Implementation
    {
        public Type ImplementationType { get; }

        public TTL TimeToLive { get; }

        public ServiceImplementations Number { get; }

        public Implementation(Type implementationType, TTL ttl, ServiceImplementations number)
        {
            ImplementationType = implementationType;
            TimeToLive = ttl;
            Number = number;
        }
    }
}
