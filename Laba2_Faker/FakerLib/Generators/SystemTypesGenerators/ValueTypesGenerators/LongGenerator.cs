using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    public class LongGenerator : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            byte[] buf = new byte[8];
            context.Randomizer.NextBytes(buf);
            return (long)BitConverter.ToInt64(buf, 0);
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(long);
        }
    }
}
