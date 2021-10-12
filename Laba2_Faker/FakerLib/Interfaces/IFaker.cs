using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Interfaces
{
    /// <summary>
    /// Interface for Faker
    /// </summary>
    public interface IFaker
    {
        T Create<T>();
    }
}
