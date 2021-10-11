using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class SByteGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (sbyte)context.Randomizer.Next();
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(sbyte);
        }
    }
}
