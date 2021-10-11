using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib;

namespace FakerLib.Generators.Service
{
    public class GeneratorContext
    {
        public readonly Random Randomizer;
        public readonly Type GeneratedType;
        public Faker Faker;

        public GeneratorContext(Random randomizer, Type generatedType, Faker faker)
        {
            Randomizer = randomizer;
            GeneratedType = generatedType;
            Faker = faker;
        }
    }
}
