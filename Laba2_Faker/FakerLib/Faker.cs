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
    /// <summary>
    /// Faker class
    /// </summary>
    public class Faker : IFaker
    {
        //Supported generators
        private List<IGenerator> generators;

        //Supported custom generators
        private Dictionary<MemberInfo, IGenerator> customGenerators;

        //Stack for storage nested types
        private Stack<Type> nestedTypesStack;

        /// <summary>
        /// Create a new instance of Faker
        /// </summary>
        public Faker()
        {
            //Supported generators
            generators = new List<IGenerator> {
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

            //Load generators from plugins
            PluginLoader pluginLoader = new PluginLoader();
            var pluginGenerators = pluginLoader.LoadPlugins();

            foreach (IGenerator generator in pluginGenerators)
            {
                generators.Add(generator);
            }

            customGenerators = new Dictionary<MemberInfo, IGenerator>();
            nestedTypesStack = new Stack<Type>();
        }

        /// <summary>
        /// Create a new instance of Faker
        /// </summary>
        /// <param name="fakerConfig">FakerConfig object to configure Faker</param>
        public Faker(FakerConfig fakerConfig) :
            this()
        {
            customGenerators = fakerConfig.CustomGenerators;
        }

        /// <summary>
        /// Create DTO object
        /// </summary>
        /// <typeparam name="T">Type of DTO object</typeparam>
        /// <returns>DTO object</returns>
        public T Create<T>()
        {
            nestedTypesStack.Clear();
            nestedTypesStack.Push(typeof(T));

            return (T)Create(typeof(T));
        }

        /// <summary>
        /// Create DTO object
        /// </summary>
        /// <param name="type">Type of DTO object</param>
        /// <returns>DTO object</returns>
        internal object Create(Type type)
        {
            IGenerator generator;

            //Chose type of generator
            if ((generator = FindCustomGenerator(type)) != null)
            {
                return generator.Generate(new GeneratorContext(new Random(), type, this));
            }
            else if ((generator = FindGenerator(type)) != null)
            {
                return generator.Generate(new GeneratorContext(new Random(), type, this));
            }
            else if (IsCustomClass(type))
            {
                var DTOObject = GetInstance(type);

                if (DTOObject != null)
                {
                    SetFields(ref DTOObject);
                    SetProperties(ref DTOObject);
                }

                return DTOObject;
            }

            return null;
        }

        /// <summary>
        /// Get instance of DTO object
        /// </summary>
        /// <param name="type">Type of DTO object</param>
        /// <returns>DTO object</returns>
        private object GetInstance(Type type)
        {
            ConstructorInfo constructor = GetMaxCountParamsConstructor(type);
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] constructorParameters = GenerateParameters(parameters);
            object generatedObject = constructor.Invoke(constructorParameters);

            return generatedObject;
        }

        /// <summary>
        /// Generate parameters for constructor of DTO object
        /// </summary>
        /// <param name="parameters">Parameters of DTO object constructor</param>
        /// <returns>Array of generated parameters</returns>
        private object[] GenerateParameters(ParameterInfo[] parameters)
        {
            object[] generatedParameters = new object[parameters.Length];

            int counter = 0;
            foreach (var parameter in parameters)
            {
                IGenerator generator;

                //Choose type of generator
                if ((generator = FindCustomGeneratorConstructorParams(parameter)) != null)
                {
                    generatedParameters[counter] = 
                        generator.Generate(new GeneratorContext(new Random(), parameter.ParameterType, this));
                }
                else if ((generator = FindGenerator(parameter.ParameterType)) != null)
                {
                    generatedParameters[counter] =
                        generator.Generate(new GeneratorContext(new Random(), parameter.ParameterType, this));
                }
                else if (IsCustomClass(parameter.ParameterType))
                {
                    if (!nestedTypesStack.Contains(parameter.ParameterType))
                    {
                        nestedTypesStack.Push(parameter.ParameterType);
                        generatedParameters[counter] = Create(parameter.ParameterType);
                        nestedTypesStack.Pop();
                    }
                }

                counter++;
            }

            return generatedParameters;
        }

        /// <summary>
        /// Get the most suitable constructor of DTO object with maximum count of parameters
        /// </summary>
        /// <param name="type">Type of DTO object</param>
        /// <returns>ConstructorInfo object</returns>
        private ConstructorInfo GetMaxCountParamsConstructor(Type type)
        {
            ConstructorInfo[] constructors = 
                type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            int maxCountParams = constructors[0].GetParameters().Length;
            ConstructorInfo mostSuitableConstructor = constructors[0];

            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length > maxCountParams)
                {
                    maxCountParams = constructor.GetParameters().Length;
                    mostSuitableConstructor = constructor;
                }
            }

            return mostSuitableConstructor;
        }

        /// <summary>
        /// Find generator for this type
        /// </summary>
        /// <param name="type">Type of object</param>
        /// <returns>IGenerator for this type of object or null</returns>
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

        /// <summary>
        /// Find custom generator for this type
        /// </summary>
        /// <param name="memberInfo">MemberInfo object</param>
        /// <returns>IGenerator for this type of object or null</returns>
        private IGenerator FindCustomGenerator(MemberInfo memberInfo)
        {
            foreach (KeyValuePair<MemberInfo, IGenerator> customGenerator in customGenerators)
            {
                if ((customGenerator.Key.Name.ToLower() == memberInfo.Name.ToLower()) && (memberInfo.Equals(customGenerator.Key)))
                {
                    return customGenerator.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Find custom generator for constructor parameter
        /// </summary>
        /// <param name="parameterInfo">Constructor parameter</param>
        /// <returns>IGenerator for this type of object or null</returns>
        private IGenerator FindCustomGeneratorConstructorParams(ParameterInfo parameterInfo)
        {
            IGenerator generator = null;

            foreach (KeyValuePair<MemberInfo, IGenerator> customGenerator in customGenerators)
            {
                string convertedType;

                var member = customGenerator.Key;
                switch (member.MemberType)
                {
                    case MemberTypes.Event:
                        convertedType = ((EventInfo)member).EventHandlerType.ToString();
                        break;
                    case MemberTypes.Field:
                        convertedType = ((FieldInfo)member).FieldType.ToString();
                        break;
                    case MemberTypes.Method:
                        convertedType = ((MethodInfo)member).MemberType.ToString();
                        break;
                    case MemberTypes.Property:
                        convertedType = ((PropertyInfo)member).PropertyType.ToString();
                        break;
                    default:
                        return null;
                }

                if ((customGenerator.Key.Name.ToLower() == parameterInfo.Name.ToLower()) &&
                    parameterInfo.ParameterType.ToString() == convertedType &&
                    parameterInfo.Member.DeclaringType.ToString() == customGenerator.Key.DeclaringType.ToString())
                {
                    generator = customGenerator.Value;

                    return generator;
                }
            }

            return null;
        }

        /// <summary>
        /// Set public fields of DTO object
        /// </summary>
        /// <param name="DTOObject">DTO object</param>
        private void SetFields(ref object DTOObject)
        {
            FieldInfo[] fields = DTOObject.GetType().GetFields();
            foreach (FieldInfo field in fields)
            {
                IGenerator generator;

                if ((generator = FindCustomGenerator(field)) != null)
                {
                    field.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), field.FieldType, this)));
                }
                else if ((generator = FindGenerator(field.FieldType)) != null)
                {
                    field.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), field.FieldType, this)));
                }
                else if (IsCustomClass(field.FieldType))
                {
                    if (!nestedTypesStack.Contains(field.FieldType))
                    {
                        nestedTypesStack.Push(field.FieldType);
                        field.SetValue(DTOObject, Create(field.FieldType));
                        nestedTypesStack.Pop();
                    }
                }
            }
        }

        /// <summary>
        /// Set public properties for DTO object
        /// </summary>
        /// <param name="DTOObject">DTO object</param>
        private void SetProperties(ref object DTOObject)
        {
            PropertyInfo[] properties = DTOObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                IGenerator generator;

                if ((generator = FindCustomGenerator(property)) != null)
                {
                    property.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), property.PropertyType, this)));
                }
                else if ((generator = FindGenerator(property.PropertyType)) != null)
                {
                    property.SetValue(DTOObject,
                        generator.Generate(new GeneratorContext(new Random(), property.PropertyType, this)));
                }
                else if (IsCustomClass(property.PropertyType))
                {
                    if (!nestedTypesStack.Contains(property.PropertyType))
                    {
                        nestedTypesStack.Push(property.PropertyType);
                        property.SetValue(DTOObject, Create(property.PropertyType));
                        nestedTypesStack.Pop();
                    }
                }
            }
        }

        /// <summary>
        /// Check for correct custom class
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>bool</returns>
        private bool IsCustomClass(Type type)
        {
            if ((type.IsClass) && (!type.IsArray) && (FindGenerator(type) == null))
            {
                return true;
            }

            return false;
        }
    }
}
