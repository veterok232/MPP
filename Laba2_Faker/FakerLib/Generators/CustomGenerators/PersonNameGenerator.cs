using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;

namespace FakerLib.Generators.CustomGenerators
{
    public class PersonNameGenerator : IGenerator
    {
        private readonly List<string> nameSet;

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

        object IGenerator.Generate(GeneratorContext context)
        {
            return nameSet[context.Randomizer.Next(nameSet.Count)];
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return (type == typeof(string));
        }
    }
}
