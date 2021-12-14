using System;

namespace DependencyInjectionContainer.Model
{
    [Flags]
    public enum ServiceImplementations
    {
        None = 1,
        First = 2,
        Second = 4,
        Any = None | First | Second
    }
}
