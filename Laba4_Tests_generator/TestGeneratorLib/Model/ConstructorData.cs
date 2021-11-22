using System.Collections.Generic;

namespace TestGeneratorLib.Model.Class
{
    /// <summary>
    ///     Constructor information
    /// </summary>
    public class ConstructorData
    {
        /// <summary>
        ///     Constructor name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Constructor parameters
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// 
        /// <param name="name">Constructor name</param>
        /// <param name="parameters">Constructor parameters</param>
        public ConstructorData(string name, Dictionary<string, string> parameters)
        {
            Name = name;
            Parameters = parameters;
        }
    }
}
