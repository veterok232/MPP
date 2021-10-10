using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Interfaces;
using Generators.Service;


namespace Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class UIntGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (uint)context.Randomizer.NextDouble();
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(uint);
        }
    }
}
