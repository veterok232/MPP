using Generators.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators.Interfaces
{
    public interface IGenerator
    {
        object Generate(GeneratorContext context);
        bool isTypeCompatible(Type type);
    }
}
