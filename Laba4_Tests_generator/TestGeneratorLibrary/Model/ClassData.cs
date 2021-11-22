using System.Collections.Generic;
using TestGeneratorLib.Model.Class;

namespace TestGeneratorLib.Model
{
    /// <summary>
    ///     Class information
    /// </summary>
    public class ClassData
    {
        /// <summary>
        ///     Class name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Constructions information
        /// </summary>
        public List<ConstructorData> ConstructorsData { get; private set; }

        /// <summary>
        ///     Methods information
        /// </summary>
        public List<MethodData> MethodsData { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// 
        /// <param name="name">Class name</param>
        /// <param name="constructorsData">Constructions information</param>
        /// <param name="methodsData">Methods information</param>
        public ClassData(string name, List<ConstructorData> constructorsData, List<MethodData> methodsData)
        {
            Name = name;
            ConstructorsData = constructorsData;
            MethodsData = methodsData;
        }
    }
}
