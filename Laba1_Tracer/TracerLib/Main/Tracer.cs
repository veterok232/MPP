using System;
using System.Threading;
using System.Diagnostics;
using TracerLib.Interface;
using TracerLib.Model;

namespace TracerLib.Main
{
    /// <summary>
    /// Tracer class for trace methods and measuring their executing time
    /// </summary>
    public class Tracer : ITracer
    {
        // TraceService object
        private TraceService _traceService;

        public Tracer() 
        {
            _traceService = new TraceService();
        }

        /// <summary>
        /// Start trace method (put at the begin of the method)
        /// </summary>
        public void StartTrace()
        {
            //Get info about the calling method
            StackTrace st = new StackTrace(true);
            StackFrame stFrame = st.GetFrame(1);

            //Get current thread ID
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            //Create or get ThreadTraceService obj
            ThreadTraceService threadTraceS;
            if ((threadTraceS = _traceService.GetThreadTrace(currentThreadID)) == null)
            {
                threadTraceS = new ThreadTraceService(_traceService, currentThreadID);
                _traceService.AddThreadTrace(threadTraceS);
            }

            //Create MethodTraceService obj
            MethodTraceService methodTraceS = new MethodTraceService(_traceService, stFrame.GetMethod().Name, stFrame.GetMethod().DeclaringType.Name, currentThreadID);
            threadTraceS.PushMethodTrace(methodTraceS);      

            //Run stopwatch
            methodTraceS.RunStopwatch();
        }

        /// <summary>
        /// Stop trace method (put at the end of the method)
        /// </summary>
        public void StopTrace()
        {
            //Get current thread ID
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            //Get current ThreadTraceService obj
            ThreadTraceService threadTraceS;
            if ((threadTraceS = _traceService.GetThreadTrace(currentThreadID)) == null)
            {
                Console.WriteLine("Error! This thread does not exist!");
                return;
            }

            //Pop MethodTraceService into callstack of current thread
            MethodTraceService methodTrace = threadTraceS.PopMethodTrace();

            //Stop stopwatch
            methodTrace.StopStopwatch();
        }

        /// <summary>
        /// Get results of Tracer
        /// </summary>
        /// <returns>TraceResult object</returns>
        public TraceResult GetTraceResult()
        {
            return new TraceResult(_traceService.GetTraceInfo());
        }
    }
}
