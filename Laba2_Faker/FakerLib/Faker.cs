using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FakerLib.Interfaces;
using FakerLib.Generators.Service;
using FakerLib.Generators.Interfaces;
using FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators;
using FakerLib.Generators.SystemTypesGenerators.ReferenceTypesGenerators;
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
            new ListGenerator(),
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
            IGenerator generator = FindGenerator(type);

            if (generator != null)
            {
                return generator.Generate(new GeneratorContext(new Random(), type, this));
            }

            var DTOObject = GetInstance(type);

            if (DTOObject != null)
            {
                SetFields(ref DTOObject);
                SetProperties(ref DTOObject);
            }

            return DTOObject;
        }

        private object GetInstance(Type type)
        {
            ConstructorInfo constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.FirstOrDefault();
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] constructorParameters = GenerateParameters(parameters);
            object generatedObject = constructor.Invoke(constructorParameters);

            return generatedObject;

            /*PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                IGenerator generator = FindGenerator(property.PropertyType);

                if (generator != null)
                {
                    property.SetValue(generatedObject, 
                        generator.Generate(new GeneratorContext(new Random(), property.PropertyType, this)));
                }
                else
                {
                    property.SetValue(generatedObject, Create(property.PropertyType));
                }
            }

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                IGenerator generator = FindGenerator(field.FieldType);

                if (generator != null)
                {
                    field.SetValue(generatedObject,
                        generator.Generate(new GeneratorContext(new Random(), field.FieldType, this)));
                }
                else
                {
                    field.SetValue(generatedObject, Create(field.FieldType));
                }
            }

            return generatedObject;*/
        }

        private object[] GenerateParameters(ParameterInfo[] parameters)
        {
            object[] generatedParameters = new object[parameters.Length];

            int counter = 0;
            foreach (var parameter in parameters)
            {
                IGenerator generator = FindGenerator(parameter.ParameterType);

                if (generator != null)
                {
                    generatedParameters[counter] = generator.Generate(new GeneratorContext(new Random(), parameter.ParameterType, this));
                }
            }

            return generatedParameters;
        }

        private IGenerator FindGenerator(Type type)
        {
            foreach (IGenerator generator in generators)
            {
                if (generator.isTypeCompatible(type))
                {
                    return generator;
                }
            }

            return null;
        }

        private void SetFields(ref object DTOObject)
        {
            FieldInfo[] fields = DTOObject.GetType().GetFields();
            foreach (FieldInfo field in fields)
            {
                IGenerator generator = FindGenerator(field.FieldType);

                if (generator != null)
                {
                    field.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), field.FieldType, this)));
                }
                else
                {
                    field.SetValue(DTOObject, Create(field.FieldType));
                }
            }
        }

        private void SetProperties(ref object DTOObject)
        {
            PropertyInfo[] properties = DTOObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                IGenerator generator = FindGenerator(property.PropertyType);

                if (generator != null)
                {
                    property.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), property.PropertyType, this)));
                }
                else
                {
                    property.SetValue(DTOObject, Create(property.PropertyType));
                }
            }
        }
    }
}
