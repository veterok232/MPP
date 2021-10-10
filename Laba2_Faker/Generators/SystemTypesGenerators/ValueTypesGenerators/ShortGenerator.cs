using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Interfaces;
using Generators.Service;


namespace Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class ShortGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (short)context.Randomizer.Next();
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(short);
        }
    }
}
