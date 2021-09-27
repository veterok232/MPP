using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    /// <summary>
    /// Class for writing SerializedTraceResult to console or file.
    /// </summary>
    public static class ResultsWriter
    {
        /// <summary>
        /// Write SerializedTraceResult to console.
        /// </summary>
        /// <param name="serializedTraceResult">SerializedTraceResult object</param>
        public static void WriteToConsole(SerializedTraceResult serializedTraceResult)
        {
            Console.WriteLine(serializedTraceResult.serializedData);
        }

        /// <summary>
        /// Write SerializedTraceResult to file
        /// </summary>
        /// <param name="serializedTraceResult">SerializedTraceResult object</param>
        /// <param name="pathToFile">Path to file</param>
        public static bool WriteToFile(SerializedTraceResult serializedTraceResult, string pathToFile)
        {
            string fileExt;

            switch (serializedTraceResult.SerializationFormat)
            {
                case SerializedTraceResult.TSerializationFormat.JSON:
                    fileExt = "json";
                    break;
                case SerializedTraceResult.TSerializationFormat.XML:
                    fileExt = "xml";
                    break;
                default:
                    fileExt = "txt";
                    break;
            }

            try
            {
                using (FileStream fstream = new FileStream($"{pathToFile}\\TraceResults.{fileExt}", FileMode.OpenOrCreate))
                {
                    byte[] data = Encoding.Default.GetBytes(serializedTraceResult.serializedData);
                    fstream.Write(data, 0, data.Length);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
