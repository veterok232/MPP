using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Interfaces;
using Generators.Service;


namespace Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class CharGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (char)context.Randomizer.Next('A', 'z');
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(char);
        }
    }
}
