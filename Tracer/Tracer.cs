using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        static object locker = new object();

        public Tracer()
        {

        }
        
        public void StartTrace()
        {
            StackTrace st = new StackTrace(true);
            StackFrame stFrame = st.GetFrame(1);

            ThreadTraceService threadTraceS;

            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            if ((threadTraceS = TraceService.GetThreadTrace(currentThreadID)) == null)
            {
                threadTraceS = new ThreadTraceService(currentThreadID);
                TraceService.AddThreadTrace(threadTraceS);
            }

            MethodTraceService methodTraceS = new MethodTraceService(stFrame.GetMethod().Name, stFrame.GetMethod().DeclaringType.Name, currentThreadID);
            lock (locker)
            {
                threadTraceS.PushMethodTrace(methodTraceS);

                //Console.WriteLine($"Thread: {currentThreadID}");
                //Console.WriteLine($"Method: {stFrame.GetMethod().Name}");
                //Console.WriteLine($"Class: {stFrame.GetMethod().DeclaringType.Name}");

                //Console.WriteLine();
            }          

            methodTraceS.RunStopwatch();
        }

        public void StopTrace()
        {
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            ThreadTraceService threadTraceS;

            if ((threadTraceS = TraceService.GetThreadTrace(currentThreadID)) == null)
            {
                //Console.WriteLine("Error! This thread does not exist!");
                return;
            }

            MethodTraceService methodTrace = threadTraceS.PopMethodTrace();
            methodTrace.StopStopwatch();

            /*TimeSpan tsTimeMethod = methodTrace.Time;
            string elapsedTimeMethod = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            tsTimeMethod.Hours, tsTimeMethod.Minutes, tsTimeMethod.Seconds,
            tsTimeMethod.Milliseconds);

            Console.WriteLine($"Method time: {elapsedTimeMethod}");

            TimeSpan tsTime = threadTraceS.Time;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            tsTime.Hours, tsTime.Minutes, tsTime.Seconds,
            tsTime.Milliseconds);

            Console.WriteLine($"Thread time: {elapsedTime}");*/
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(TraceService.GetTraceInfo());
        }
    }
}
