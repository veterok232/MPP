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
    /// UIntGenerator class
    /// </summary>
    public class UIntGenerator : IGenerator
    {
        /// <summary>
        /// Generate uint object
        /// </summary>
        /// <param name="context">GeneratorContext object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            return (uint)(context.Randomizer.Next(1 << 30)) << 2 | (uint)(context.Randomizer.Next(1 << 2));
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(uint);
        }
    }
}
