using System;

namespace DependencyInjectionContainer.Model
{
    /// <summary>
    ///     Implementation number
    /// </summary>
    [Flags]
    public enum ServiceImplementations
    {
        None = 1,
        First = 2,
        Second = 4,
        Any = None | First | Second
    }
}
