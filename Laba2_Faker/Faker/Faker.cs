using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Interfaces;
using Generators.Service;
using Generators.Interfaces;
using Generators.ValueTypesGenerators;
using Generators.ReferenceTypeGenerators;
using FakerLib.Service;

namespace FakerLib
{
    public class Faker : IFaker
    {
        private List<IGenerator> generators = new List<IGenerator> {
            new BoolGenerator(),
            new ByteGenerator(),
            new DecimalGenerator(),
            new DoubleGenerator(),
            new FloatGenerator(),
            new LongGenerator(),
            new SByteGenerator(),
            new ShortGenerator(),
            new UIntGenerator(),
            new ULongGenerator(),
            new UShortGenerator(),
            new DateTimeGenerator(),
            new StringGenerator(),
        };

        public Faker()
        {
            PluginLoader pluginLoader = new PluginLoader();
            var pluginGenerators = pluginLoader.LoadPlugins();

            foreach (IGenerator generator in pluginGenerators)
            {
                generators.Add(generator);
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        internal object Create(Type type)
        {
            object DTOObject = null;

            foreach (IGenerator generator in generators)
            {
                if (generator.isTypeCompatible(type))
                {
                    return generator.Generate(new GeneratorContext(new Random(), type));
                }
            }

            return DTOObject;
        }
    }
}
