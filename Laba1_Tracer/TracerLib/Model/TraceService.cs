using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using TracerLib.Service;

namespace TracerLib.Model
{
    /// <summary>
    /// Service class for Tracer.
    /// Contains instruments for Tracer
    /// </summary>
    public class TraceService
    {
        //Threads info dictionary
        private ConcurrentDictionary<int, ThreadTraceService> _threadsInfo;

        /// <summary>
        /// Create a new instance of TraceService.
        /// </summary>
        public TraceService()
        {
            _threadsInfo = new ConcurrentDictionary<int, ThreadTraceService>();
        }

        /// <summary>
        /// Try get ThreadTraceService object by thread ID.
        /// </summary>
        /// <param name="threadID">Thread ID</param>
        /// <returns>ThreadTraceService object if exists else returns null</returns>
        public ThreadTraceService GetThreadTrace(int threadID)
        {
            ThreadTraceService result = null;
            _threadsInfo.TryGetValue(threadID, out result);
            return result;
        }

        /// <summary>
        /// Try add ThreadTraceService object to threads dictionary.
        /// </summary>
        /// <param name="threadTraceS">ThreadTraceService object</param>
        /// <returns>true if add is successful else returns false</returns>
        public bool AddThreadTrace(ThreadTraceService threadTraceS)
        {
            return _threadsInfo.TryAdd(threadTraceS.ID, threadTraceS);
        }

        /// <summary>
        /// Adds to the thread execution time with ID = threadID time value.
        /// </summary>
        /// <param name="threadID">Thread ID</param>
        /// <param name="time">Time is needed to add</param>
        /// <returns>true if add is successful else returns false</returns>
        public bool AddThreadTimeExec(int threadID, TimeSpan time)
        {
            ThreadTraceService threadTrace = null;
            if (!_threadsInfo.TryGetValue(threadID, out threadTrace))
                return false;

            threadTrace.Time = threadTrace.Time.Add(time);
            return true;
        }

        /// <summary>
        /// Get information about all traces threads
        /// </summary>
        /// <returns>Information about all traces threads</returns>
        public ConcurrentDictionary<int, ThreadTraceService> GetTraceInfo()
        {
            return _threadsInfo;
        }
    }

    /// <summary>
    /// Class contains trace info of thread.
    /// </summary>
    public class ThreadTraceService
    {
        // Locker object
        private static object locker = new object();

        private TraceService _traceService;
        /// <summary>
        /// Thread ID
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Thread executing time
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Thread callstack for trace method calls 
        /// </summary>
        public ConcurrentStack<MethodTraceService> MethodsInfo;
        /// <summary>
        /// Tree for storing structure of calls traced methods
        /// </summary>
        public TreeNode<MethodTraceService> MethodsTree;

        /// <summary>
        /// Create new instanse of ThreadTraceService
        /// </summary>
        /// <param name="traceService">TraceService object</param>
        /// <param name="threadID">Thread ID</param>
        public ThreadTraceService(TraceService traceService, int threadID)
        {
            _traceService = traceService;
            ID = threadID;
            MethodsInfo = new ConcurrentStack<MethodTraceService>();
            MethodsTree = new TreeNode<MethodTraceService>(null);
        }

        /// <summary>
        /// Push method trace to callstack of this thread.
        /// </summary>
        /// <param name="methodTraceS">MethodTraceService object</param>
        public void PushMethodTrace(MethodTraceService methodTraceS)
        {
            lock (locker)
            {
                AddToMethodsTree(methodTraceS);
            }
            
            MethodsInfo.Push(methodTraceS);
        }

        /// <summary>
        /// Peek last pushed method from callstack of this thread.
        /// </summary>
        /// <returns>MethodTraceService object if callstack is not empty else returns false</returns>
        public MethodTraceService PeekMethodTrace()
        {
            MethodTraceService methodTraceS;
            if (MethodsInfo.TryPeek(out methodTraceS))
                return methodTraceS;
            else
                return null;
        }

        /// <summary>
        /// Pop last pushed method from callstack of this thread.
        /// </summary>
        /// <returns>MethodTraceService object if callstack is not empty else returns false</returns>
        public MethodTraceService PopMethodTrace()
        {
            MethodTraceService methodTraceS;
            if (MethodsInfo.TryPop(out methodTraceS))
                return methodTraceS;
            else
                return null;
        }

        /// <summary>
        /// Add MethodTraceService object to call tree of this thread
        /// </summary>
        /// <param name="methodTrace"></param>
        public void AddToMethodsTree(MethodTraceService methodTrace)
        {
            TreeNode<MethodTraceService> currNode = new TreeNode<MethodTraceService>(methodTrace);
            TreeNode<MethodTraceService> parentNode;

            MethodTraceService parentMethodTrace;
            if (MethodsInfo.TryPeek(out parentMethodTrace))
            {
                parentNode = parentMethodTrace.Node;
            }
            else
            {
                parentNode = MethodsTree;
            }

            methodTrace.Node = parentNode.AddChild(methodTrace);
        }
    }

    /// <summary>
    /// Class contains trace info of method.
    /// </summary>
    public class MethodTraceService
    {
        private TraceService _traceService;
        private int _threadID;
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Node matched to this MethodTraceService object in call tree
        /// </summary>
        public TreeNode<MethodTraceService> Node { get; set; }

        /// <summary>
        /// Name of traced method
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Class of traced method
        /// </summary>
        public string ClassName { get; private set; }
        /// <summary>
        /// Executing time of traced method
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Create new instance of MethodTraceService object
        /// </summary>
        /// <param name="traceService">TraceService object</param>
        /// <param name="name">Name of method</param>
        /// <param name="className">Class name of method</param>
        /// <param name="threadID">Thread ID</param>
        public MethodTraceService(TraceService traceService, string name, string className, int threadID)
        {
            _traceService = traceService;
            _threadID = threadID;
            Name = name;
            ClassName = className;
        }

        /// <summary>
        /// Run stopwatch to start measuring executing time of the method.
        /// </summary>
        public void RunStopwatch()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Stop stopwatch to stop measuring executing time of the method.
        /// </summary>
        public void StopStopwatch()
        {
            _stopwatch.Stop();
            Time = _stopwatch.Elapsed;
            if (Node.Parent.Data == null)
            {
                _traceService.AddThreadTimeExec(_threadID, Time);
            }
        }
    }
}
