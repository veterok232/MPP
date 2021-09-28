using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TracerLib.Model;

namespace ResultsWriter.Model
{
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
        [XmlAttribute("time")]
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
}
