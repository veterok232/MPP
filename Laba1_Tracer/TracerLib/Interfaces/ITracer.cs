using System;
using TracerLib.Model;

namespace TracerLib.Interface
{
    /// <summary>
    /// Interface for Tracer
    /// </summary>
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}
