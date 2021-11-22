using System.Collections.Generic;

namespace TestGeneratorLib.Model.Class
{
    /// <summary>
    ///     Method information
    /// </summary>
    public class MethodData
    {
        /// <summary>
        ///     Method name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Method parameters
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        ///     Method return type
        /// </summary>
        public string ReturnType { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// 
        /// <param name="name">Method name</param>
        /// <param name="parameters">Method parameters</param>
        /// <param name="returnType">Method return type</param>
        public MethodData(string name, Dictionary<string, string> parameters, string returnType)
        {
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }
    }
}
