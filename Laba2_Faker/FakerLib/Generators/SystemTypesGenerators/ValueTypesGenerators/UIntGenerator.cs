using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class UIntGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (uint)(context.Randomizer.Next(1 << 30)) << 2 | (uint)(context.Randomizer.Next(1 << 2));
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(uint);
        }
    }
}
