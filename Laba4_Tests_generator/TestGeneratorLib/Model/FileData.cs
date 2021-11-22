using System.Collections.Generic;

namespace TestGeneratorLib.Model
{
    /// <summary>
    ///     File information (all classes)
    /// </summary>
    public class FileData
    {
        /// <summary>
        ///     All classes in file
        /// </summary>
        public List<ClassData> ClassesData { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// 
        /// <param name="classesData">All classes in file</param>
        public FileData(List<ClassData> classesData)
        {
            ClassesData = classesData;
        }
    }
}
