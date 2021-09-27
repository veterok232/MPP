using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Collections;

namespace Tracer
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
        #region TraceResult non-readonly class for serialization 
        /// <summary>
        /// Class with non read-only data of trace results.
        /// </summary>
        [Serializable]
        [XmlRoot("root")]
        [JsonObject]
        public class TraceResultNonReadOnly
        {
            /// <summary>
            /// List with info about traced threads.
            /// </summary>
            [JsonProperty("threads")]
            [XmlElement("thread")]
            public List<ThreadTraceResultNonReadOnly> ThreadsInfo;

            /// <summary>
            /// Create a new instance of TraceResult.
            /// </summary>
            public TraceResultNonReadOnly() { }

            /// <summary>
            /// Create a new instance of TraceResult.
            /// </summary>
            /// <param name="traceResult">TraceResult object</param>
            public TraceResultNonReadOnly(TraceResult traceResult)
            {
                ThreadTraceResultNonReadOnly[] _threadsInfo = new ThreadTraceResultNonReadOnly[traceResult.ThreadsInfo.Count];

                int i = 0;
                foreach (ThreadTraceResult threadTraceResult in traceResult)
                {
                    _threadsInfo[i++] = new ThreadTraceResultNonReadOnly(threadTraceResult);
                }

                ThreadsInfo = new List<ThreadTraceResultNonReadOnly>(_threadsInfo);
            }
        }

        /// <summary>
        /// Non-readonly results of traced thread.
        /// </summary>
        [Serializable]
        [JsonObject]
        public class ThreadTraceResultNonReadOnly
        {
            /// <summary>
            /// Thread ID.
            /// </summary>
            [JsonProperty("id")]
            [XmlAttribute("id")]
            public string ID;

            /// <summary>
            /// Thread executing time.
            /// </summary>
            [JsonProperty("time")]
            [XmlAttribute("name")]
            public string Time;

            /// <summary>
            /// List of MethodTraceResult.
            /// </summary>
            [JsonProperty("methods")]
            [XmlElement("method")]
            public List<MethodTraceResultNonReadOnly> MethodsInfo;

            /// <summary>
            /// Create a new instance of ThreadTraceResultNonReadOnly.
            /// </summary>
            public ThreadTraceResultNonReadOnly() { }

            /// <summary>
            /// Create a new instance of ThreadTraceResultNonReadOnly.
            /// </summary>
            /// <param name="threadTraceResult">ThreadTraceResult object</param>
            public ThreadTraceResultNonReadOnly(ThreadTraceResult threadTraceResult)
            {
                ID = threadTraceResult.ID;
                Time = threadTraceResult.Time;

                MethodTraceResultNonReadOnly[] _methodsInfo;

                if (threadTraceResult.MethodsInfo.Count > 0)
                {
                    _methodsInfo = new MethodTraceResultNonReadOnly[threadTraceResult.MethodsInfo.Count];

                    int i = 0;
                    foreach (MethodTraceResult methodTraceResult in threadTraceResult)
                    {
                        _methodsInfo[i++] = new MethodTraceResultNonReadOnly(methodTraceResult);
                    }

                    MethodsInfo = new List<MethodTraceResultNonReadOnly>(_methodsInfo);
                }
                else
                {
                    _methodsInfo = new MethodTraceResultNonReadOnly[0];
                    MethodsInfo = new List<MethodTraceResultNonReadOnly>(_methodsInfo);
                }
            }
        }

        /// <summary>
        /// Non read-only results of traced method.
        /// </summary>
        [Serializable]
        [JsonObject]
        public class MethodTraceResultNonReadOnly
        {
            /// <summary>
            /// Method name
            /// </summary>
            [JsonProperty("name")]
            [XmlAttribute("name")]
            public string Name;

            /// <summary>
            /// Method class name
            /// </summary>
            [JsonProperty("class")]
            [XmlAttribute("class")]
            public string ClassName;

            /// <summary>
            /// Method executing time
            /// </summary>
            [JsonProperty("time")]
            [XmlAttribute("time")]
            public string Time;

            /// <summary>
            /// List with traced methods
            /// </summary>
            [JsonProperty("methods")]
            [XmlElement("method")]
            public List<MethodTraceResultNonReadOnly> MethodsInfo;

            /// <summary>
            /// Create a new instance of MethodTraceResultNonReadOnly.
            /// </summary>
            public MethodTraceResultNonReadOnly() { }

            /// <summary>
            /// Create a new instance of MethodTraceResultNonReadOnly.
            /// </summary>
            /// <param name="methodTraceResult">MethodTraceResult object</param>
            public MethodTraceResultNonReadOnly(MethodTraceResult methodTraceResult)
            {
                Name = methodTraceResult.Name;
                ClassName = methodTraceResult.ClassName;
                Time = methodTraceResult.Time;

                MethodTraceResultNonReadOnly[] _methodsInfo;

                if (methodTraceResult.MethodsInfo.Count > 0)
                {
                    _methodsInfo = new MethodTraceResultNonReadOnly[methodTraceResult.MethodsInfo.Count];

                    int i = 0;
                    foreach (MethodTraceResult _methodTraceResult in methodTraceResult)
                    {
                        _methodsInfo[i++] = new MethodTraceResultNonReadOnly(_methodTraceResult);
                    }

                    MethodsInfo = new List<MethodTraceResultNonReadOnly>(_methodsInfo);
                }
                else
                {
                    _methodsInfo = new MethodTraceResultNonReadOnly[0];
                    MethodsInfo = new List<MethodTraceResultNonReadOnly>(_methodsInfo);
                }
            }
        }
        #endregion

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
