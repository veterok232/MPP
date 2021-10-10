using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators.Interfaces;
using Generators.Service;

namespace Generators.ReferenceTypeGenerators
{
    public class StringGenerator : IGenerator
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int MIN_LENGTH = 1;
        private const int MAX_LENGTH = 100;

        object IGenerator.Generate(GeneratorContext context)
        {
            return new string(Enumerable.Repeat(ALPHABET, context.Randomizer.Next(MIN_LENGTH, MAX_LENGTH))
              .Select(s => s[context.Randomizer.Next(s.Length)]).ToArray());
        }

        bool IGenerator.isTypeCompatible(Type type)
        {
            return (type == typeof(string));
        }
    }
}
