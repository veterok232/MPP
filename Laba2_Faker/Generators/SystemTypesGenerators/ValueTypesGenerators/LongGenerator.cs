using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Interfaces;
using Generators.Service;


namespace Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class LongGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (long)context.Randomizer.NextDouble();
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(long);
        }
    }
}
