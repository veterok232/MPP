using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using FakerLib.Interfaces;
using FakerLib.Generators.Interfaces;

namespace FakerLib
{
    /// <summary>
    /// FakerConfig class
    /// </summary>
    public class FakerConfig : IFakerConfig
    {
        /// <summary>
        /// Dictionary with custom generators for DTO
        /// </summary>
        public Dictionary<MemberInfo, IGenerator> CustomGenerators { get; private set; }

        /// <summary>
        /// Create a new instance of FakerConfig
        /// </summary>
        public FakerConfig()
        {
            CustomGenerators = new Dictionary<MemberInfo, IGenerator>();
        }

        /// <summary>
        /// Add new config for Faker
        /// </summary>
        /// <typeparam name="DTObjectType">DTO type</typeparam>
        /// <typeparam name="MemberType">Member base type</typeparam>
        /// <typeparam name="GeneratorType">Generator type</typeparam>
        /// <param name="expression">Config expression</param>
        public void Add<DTObjectType, MemberType, GeneratorType>(Expression<Func<DTObjectType, MemberType>> expression)
            where DTObjectType : class
            where GeneratorType : IGenerator
        {
            // Get class field or property
            Expression eBody = expression.Body;
            if (eBody.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Invalid config expression!");
            }

            // Get generator with current type
            IGenerator generator = (IGenerator)Activator.CreateInstance(typeof(GeneratorType));
            if (!generator.isTypeCompatible(typeof(MemberType)))
            {
                throw new ArgumentException("Invalid generator type!");
            }

            // Add new config expression to dictionary
            CustomGenerators.Add(((MemberExpression)eBody).Member, generator);
        }
    }
}
