using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace Plugins.IntGeneratorPlugin
{
    /// <summary>
    /// IntGenerator plugin class
    /// </summary>
    public class IntGenerator : IGenerator
    {
        /// <summary>
        /// Generate int object
        /// </summary>
        /// <param name="context">Generator context object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            return (int)context.Randomizer.Next();
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(int);
        }
    }
}
