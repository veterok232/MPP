using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class DecimalGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return (decimal)context.Randomizer.NextDouble();
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(decimal);
        }
    }
}
