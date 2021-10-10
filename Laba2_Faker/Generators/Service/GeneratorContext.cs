using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators.Service
{
    public class GeneratorContext
    {
        public readonly Random Randomizer;
        public readonly Type GeneratedType;

        public GeneratorContext(Random randomizer, Type generatedType)
        {
            Randomizer = randomizer;
            GeneratedType = generatedType;
        }
    }
}
