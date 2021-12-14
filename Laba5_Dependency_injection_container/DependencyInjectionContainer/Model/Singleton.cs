using DependencyInjectionContainer.Interfaces;

namespace DependencyInjectionContainer.Model
{
    public class Singleton
    {
        public readonly object Instance;

        public readonly ServiceImplementations Number;

        public Singleton(object instance, ServiceImplementations number)
        {
            Instance = instance;
            Number = number;
        }
    }
}
