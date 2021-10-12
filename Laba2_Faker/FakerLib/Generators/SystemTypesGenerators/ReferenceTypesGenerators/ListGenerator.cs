using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.Service;

namespace FakerLib.Generators.SystemTypesGenerators.ReferenceTypesGenerators
{
    /// <summary>
    /// ListGenerator class
    /// </summary>
    public class ListGenerator : IGenericGenerator
    {
        private const int MIN_COUNT = 1;
        private const int MAX_COUNT = 10;

        /// <summary>
        /// Generate List<T> object
        /// </summary>
        /// <param name="context">GeneratorContext object</param>
        /// <returns>object</returns>
        object IGenerator.Generate(GeneratorContext context)
        {
            IList list = (IList)Activator.CreateInstance(context.GeneratedType);
            int countElements = context.Randomizer.Next(MIN_COUNT, MAX_COUNT + 1);
            Type elementType = context.GeneratedType.GetGenericArguments().Single();

            for (int i = 0; i < countElements; i++)
            {
                list.Add(context.Faker.Create(elementType));
                Thread.Sleep(1);
            }

            return list;
        }

        /// <summary>
        /// Check the type for generator
        /// </summary>
        /// <param name="type">Type for check</param>
        /// <returns>bool</returns>
        bool IGenerator.isTypeCompatible(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }
    }
}
