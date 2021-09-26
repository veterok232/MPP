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
    public static class Serializator
    {
        #region TraceResult non-readonly class 
        [Serializable]
        [XmlRoot("root")]
        [JsonObject]
        public class TraceResultNonReadOnly
        {
            [JsonProperty("threads")]
            [XmlElement("thread")]
            public List<ThreadTraceResultNonReadOnly> ThreadsInfo;

            public TraceResultNonReadOnly() { }

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

        [Serializable]
        [JsonObject]
        public class ThreadTraceResultNonReadOnly
        {
            [JsonProperty("id")]
            [XmlAttribute("id")]
            public string ID;

            [JsonProperty("time")]
            [XmlAttribute("name")]
            public string Time;

            [JsonProperty("methods")]
            [XmlElement("method")]
            public List<MethodTraceResultNonReadOnly> MethodsInfo;

            public ThreadTraceResultNonReadOnly() { }

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

        [Serializable]
        [JsonObject]
        public class MethodTraceResultNonReadOnly
        {
            [JsonProperty("name")]
            [XmlAttribute("name")]
            public string Name;

            [JsonProperty("class")]
            [XmlAttribute("class")]
            public string ClassName;

            [JsonProperty("time")]
            [XmlAttribute("time")]
            public string Time;

            [JsonProperty("methods")]
            [XmlElement("method")]
            public List<MethodTraceResultNonReadOnly> MethodsInfo;

            public MethodTraceResultNonReadOnly() { }

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

        public static string SerializeJson(TraceResult traceResult)
        { 
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb)) 
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                serializer.Serialize(writer, traceResult);
            }

            string serializedTraceResult = sb.ToString();
            return serializedTraceResult;
        }

        public static string SerializeTraceResultXml(TraceResult traceResult)
        {
            var traceResultSerializeable = new TraceResultNonReadOnly(traceResult);

            XmlSerializer serializer = new XmlSerializer(typeof(TraceResultNonReadOnly));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, traceResultSerializeable);
            }

            string serializedTraceResult = sb.ToString();
            return serializedTraceResult;
        }
    }
}
