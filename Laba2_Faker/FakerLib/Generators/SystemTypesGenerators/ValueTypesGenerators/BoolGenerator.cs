using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;

namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class BoolGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return context.Randomizer.Next() % 2 == 0;
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(bool);
        }
    }
}
