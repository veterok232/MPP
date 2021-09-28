using System;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using TracerLib.Model;
using ResultsWriter.Model;


namespace ResultsWriter.Service
{
    /// <summary>
    /// Class of serialized TraceResult object.
    /// </summary>
    public class SerializedTraceResult
    {
        /// <summary>
        /// Format of serialization.
        /// </summary>
        public enum TSerializationFormat
        {
            JSON = 1,
            XML = 2
        }

        /// <summary>
        /// Serialized data.
        /// </summary>
        
        public string serializedData { get; private set; }
        /// <summary>
        /// Serialization format.
        /// </summary>
        public TSerializationFormat SerializationFormat { get; private set; }

        /// <summary>
        /// Create a new instance of SerializedTraceResult.
        /// </summary>
        /// <param name="data">Serialized data</param>
        /// <param name="serializationFormat">Serialization format</param>
        public SerializedTraceResult(string data, TSerializationFormat serializationFormat)
        {
            serializedData = data;
            SerializationFormat = serializationFormat;
        }
    }

    /// <summary>
    /// Class for serialization TraceResult object
    /// </summary>
    public static class TraceResultSerializator
    {
        

        /// <summary>
        /// Serialize TraceResult object to JSON.
        /// </summary>
        /// <param name="traceResult">TraceResult object</param>
        /// <returns></returns>
        public static SerializedTraceResult SerializeJson(TraceResult traceResult)
        {
            try
            {
                var traceResultSerializeable = new TraceResultNonReadOnly(traceResult);

                var serializer = new JsonSerializer();
                serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
                serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

                StringBuilder sb = new StringBuilder();

                using (StringWriter sw = new StringWriter(sb))
                using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, traceResultSerializeable);
                }

                string serializedData = sb.ToString();

                var serializedTraceResult = new SerializedTraceResult(serializedData, SerializedTraceResult.TSerializationFormat.JSON);
                return serializedTraceResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Serialize TraceResult object to XML.
        /// </summary>
        /// <param name="traceResult">TraceResult object</param>
        /// <returns></returns>
        public static SerializedTraceResult SerializeXml(TraceResult traceResult)
        {
            try
            {
                var traceResultSerializeable = new TraceResultNonReadOnly(traceResult);

                XmlSerializer serializer = new XmlSerializer(typeof(TraceResultNonReadOnly));

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    serializer.Serialize(sw, traceResultSerializeable);
                }
                string serializedData = sb.ToString();

                var serializedTraceResult = new SerializedTraceResult(serializedData, SerializedTraceResult.TSerializationFormat.XML);
                return serializedTraceResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
