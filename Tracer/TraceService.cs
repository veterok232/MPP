using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    #region Service private TraceResult class
    public static class TraceService
    {
        private static ConcurrentDictionary<int, ThreadTraceService> _threadsInfo;

        static TraceService()
        {
            _threadsInfo = new ConcurrentDictionary<int, ThreadTraceService>();
        }

        public static ThreadTraceService GetThreadTrace(int threadID)
        {
            ThreadTraceService result = null;
            _threadsInfo.TryGetValue(threadID, out result);
            return result;
        }

        public static bool AddThreadTrace(ThreadTraceService threadTraceS)
        {
            return _threadsInfo.TryAdd(threadTraceS.ID, threadTraceS);
        }

        public static bool AddThreadTimeExec(int threadID, TimeSpan time)
        {
            ThreadTraceService threadTrace = null;
            if (!_threadsInfo.TryGetValue(threadID, out threadTrace))
                return false;

            threadTrace.Time = threadTrace.Time.Add(time);
            return true;
        }

        public static ConcurrentDictionary<int, ThreadTraceService> GetTraceInfo()
        {
            return _threadsInfo;
        }
    }

    public class ThreadTraceService
    {
        public int ID { get; private set; }
        public TimeSpan Time { get; set; }
        public ConcurrentStack<MethodTraceService> MethodsInfo;
        public TreeNode<MethodTraceService> MethodsTree;

        public ThreadTraceService(int threadID)
        {
            ID = threadID;
            MethodsInfo = new ConcurrentStack<MethodTraceService>();
            MethodsTree = new TreeNode<MethodTraceService>(null);
        }

        public bool PushMethodTrace(MethodTraceService methodTraceS)
        {
            AddToMethodsTree(methodTraceS);
            MethodsInfo.Push(methodTraceS);
            return true;
        }

        public MethodTraceService PeekMethodTrace()
        {
            MethodTraceService methodTraceS;
            if (MethodsInfo.TryPeek(out methodTraceS))
                return methodTraceS;
            else
                return null;
        }

        public MethodTraceService PopMethodTrace()
        {
            MethodTraceService methodTraceS;
            if (MethodsInfo.TryPop(out methodTraceS))
                return methodTraceS;
            else
                return null;
        }

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

    public class MethodTraceService
    {
        private int _threadID;
        private int _methodHash;
        private Stopwatch _stopwatch = new Stopwatch();

        public TreeNode<MethodTraceService> Node { get; set; }

        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public TimeSpan Time { get; set; }
        //public ConcurrentDictionary<int, MethodTraceService> MethodsInfo;

        public MethodTraceService(string name, string className, int threadID)
        {
            _threadID = threadID;
            Name = name;
            ClassName = className;
            //_methodHash = Name.GetHashCode() + ClassName.GetHashCode();
        }

        public void RunStopwatch()
        {
            _stopwatch.Start();
        }

        public void StopStopwatch()
        {
            _stopwatch.Stop();
            Time = _stopwatch.Elapsed;
            if (Node.Parent.Data == null)
            {
                TraceService.AddThreadTimeExec(_threadID, Time);
            }
        }

        public override int GetHashCode()
        {
            return _methodHash;
        }
    }
    #endregion
}
