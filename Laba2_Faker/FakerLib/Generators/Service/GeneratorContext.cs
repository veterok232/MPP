using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib;

namespace FakerLib.Generators.Service
{
    /// <summary>
    /// Object with necessary information for generators
    /// </summary>
    public class GeneratorContext
    {
        /// <summary>
        /// Generator randomizer
        /// </summary>
        public readonly Random Randomizer;

        /// <summary>
        /// Generator's generated type
        /// </summary>
        public readonly Type GeneratedType;
        /// <summary>
        /// Faker object
        /// </summary>
        public Faker Faker;

        /// <summary>
        /// Create a new instance of GeneratorContext
        /// </summary>
        /// <param name="randomizer">Random object</param>
        /// <param name="generatedType">Type object</param>
        /// <param name="faker">Faker object</param>
        public GeneratorContext(Random randomizer, Type generatedType, Faker faker)
        {
            Randomizer = randomizer;
            GeneratedType = generatedType;
            Faker = faker;
        }
    }
}
