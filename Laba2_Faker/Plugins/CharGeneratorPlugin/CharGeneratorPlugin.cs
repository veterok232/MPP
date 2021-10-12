using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;


namespace Plugins.CharGeneratorPlugin
{
    /// <summary>
    /// CharGenerator plugin class
    /// </summary>
    public class CharGenerator : IGenerator
    {
        /// <summary>
        /// Generate char object
        /// </summary>
        /// <param name="context">Generator context object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            return (char)context.Randomizer.Next('A', 'z');
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return type == typeof(char);
        }
    }
}
