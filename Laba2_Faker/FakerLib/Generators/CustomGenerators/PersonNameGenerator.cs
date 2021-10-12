using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;

namespace FakerLib.Generators.CustomGenerators
{
    /// <summary>
    /// PersonNameGenerator custom generator class
    /// </summary>
    public class PersonNameGenerator : IGenerator
    {
        /// <summary>
        /// List of allowable names
        /// </summary>
        private readonly List<string> nameSet;

        /// <summary>
        /// Create a new instance of PersonNameGenerator
        /// </summary>
        public PersonNameGenerator()
        {
            nameSet = new List<string>()
            {
                "Andrey",
                "Maksim",
                "Ivan",
                "Sasha",
                "Masha",
                "Dasha",
                "Sergey",
                "Anton"
            };
        }

        /// <summary>
        /// Generate string object
        /// </summary>
        /// <param name="context">GeneratorContext object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            return nameSet[context.Randomizer.Next(nameSet.Count)];
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return (type == typeof(string));
        }
    }
}
