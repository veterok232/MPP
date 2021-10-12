using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators
{
    /// <summary>
    /// LongGenerator class
    /// </summary>
    public class LongGenerator : IGenerator
    {
        /// <summary>
        /// Generate long object
        /// </summary>
        /// <param name="context">GeneratorContext object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            byte[] buf = new byte[8];
            context.Randomizer.NextBytes(buf);
            return (long)BitConverter.ToInt64(buf, 0);
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(long);
        }
    }
}
