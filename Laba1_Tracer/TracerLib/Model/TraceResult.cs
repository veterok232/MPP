using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.ObjectModel;
using TracerLib.Service;

namespace TracerLib.Model
{
    /// <summary>
    /// Class with read-only data of trace results.
    /// </summary>
    public class TraceResult : IEnumerable<ThreadTraceResult>
    {
        /// <summary>
        /// ReadOnlyCollection with info about traced threads.
        /// </summary>
        public readonly ReadOnlyCollection<ThreadTraceResult> ThreadsInfo;

        /// <summary>
        /// Create a new instance of TraceResult.
        /// </summary>
        public TraceResult() { }

        /// <summary>
        /// Create a new instance of TraceResult.
        /// </summary>
        /// <param name="traceResultInfo">Dictionary of ThreadTraceService object</param>
        public TraceResult(ConcurrentDictionary<int, ThreadTraceService> traceResultInfo)
        {
            ThreadTraceResult[] _threadsInfo;

            if (traceResultInfo != null)
            {
                _threadsInfo = new ThreadTraceResult[traceResultInfo.Count];

                int i = 0;
                foreach (KeyValuePair<int, ThreadTraceService> pair in traceResultInfo)
                {
                    _threadsInfo[i++] = new ThreadTraceResult(pair.Value.ID, pair.Value.Time, pair.Value.MethodsTree);
                }
            }
            else
            {
                _threadsInfo = new ThreadTraceResult[0];
            }

            ThreadsInfo = new ReadOnlyCollection<ThreadTraceResult>(_threadsInfo);
        }

        IEnumerator<ThreadTraceResult> IEnumerable<ThreadTraceResult>.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator<ThreadTraceResult> actualImplementation()
            {
                foreach (ThreadTraceResult threadTraceResult in ThreadsInfo)
                    yield return threadTraceResult;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator actualImplementation()
            {
                foreach (ThreadTraceResult threadTraceResult in ThreadsInfo)
                    yield return threadTraceResult;
            }
        }
    }

    /// <summary>
    /// Read-only results of traced thread.
    /// </summary>
    public class ThreadTraceResult : IEnumerable<MethodTraceResult>
    {
        /// <summary>
        /// Thread ID.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Thread executing time.
        /// </summary>
        public string Time { get; private set; }

        /// <summary>
        /// ReadOnlyCollection of MethodTraceResult.
        /// </summary>
        public readonly ReadOnlyCollection<MethodTraceResult> MethodsInfo;

        /// <summary>
        /// Create a new instance of ThreadTraceResult.
        /// </summary>
        public ThreadTraceResult() { }

        /// <summary>
        /// Create a new instance of ThreadTraceResult.
        /// </summary>
        /// <param name="id">Thread ID</param>
        /// <param name="time">Thread executing time</param>
        /// <param name="methodsTree">Call tree of traced method in this thread</param>
        public ThreadTraceResult(int id, TimeSpan time, TreeNode<MethodTraceService> methodsTree)
        {
            ID = id.ToString();
            Time = String.Format("{0}ms",
                            time.Hours * 3600 * 1000 + time.Minutes * 60 * 1000 + time.Seconds * 1000 + 
                            time.Milliseconds);

            MethodTraceResult[] _methodsInfo;

            if (methodsTree.Children.Count > 0)
            {
                _methodsInfo = new MethodTraceResult[methodsTree.Children.Count];

                int i = 0;
                foreach (TreeNode<MethodTraceService> node in methodsTree.Children)
                {
                    _methodsInfo[i++] = new MethodTraceResult(node.Data.Name, node.Data.ClassName, node.Data.Time, node);
                }

                MethodsInfo = new ReadOnlyCollection<MethodTraceResult>(_methodsInfo);
            }
            else
            {
                _methodsInfo = new MethodTraceResult[0];
                MethodsInfo = new ReadOnlyCollection<MethodTraceResult>(_methodsInfo);
            }
        }

        IEnumerator<MethodTraceResult> IEnumerable<MethodTraceResult>.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator<MethodTraceResult> actualImplementation()
            {
                foreach (MethodTraceResult methodTraceResult in MethodsInfo)
                    yield return methodTraceResult;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator actualImplementation()
            {
                foreach (MethodTraceResult methodTraceResult in MethodsInfo)
                    yield return methodTraceResult;
            }
        }
    }

    /// <summary>
    /// Read-only results of traced method.
    /// </summary>
    public class MethodTraceResult : IEnumerable<MethodTraceResult>
    {
        /// <summary>
        /// Method name
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Method class name
        /// </summary>
        public string ClassName { get; private set; }
       
        /// <summary>
        /// Method executing time
        /// </summary>
        public string Time { get; private set; }
        
        /// <summary>
        /// ReadOnlyCollection with traced methods
        /// </summary>
        public readonly ReadOnlyCollection<MethodTraceResult> MethodsInfo;

        /// <summary>
        /// Create a new instance of MethodTraceResult.
        /// </summary>
        public MethodTraceResult() { }

        /// <summary>
        /// Create a new instance of MethodTraceResult.
        /// </summary>
        /// <param name="name">Method name</param>
        /// <param name="className">Method class name</param>
        /// <param name="time">Method executing time</param>
        /// <param name="methodsTree">Call tree of traced method in this method/param>
        public MethodTraceResult(string name, string className, TimeSpan time, TreeNode<MethodTraceService> methodsTree)
        {
            Name = name;
            ClassName = className;
            Time = String.Format("{0}ms",
                            time.Hours * 3600 * 1000 + time.Minutes * 60 * 1000 + time.Seconds * 1000 +
                            time.Milliseconds);

            MethodTraceResult[] _methodsInfo;

            if (methodsTree.Children.Count > 0)
            {
                _methodsInfo = new MethodTraceResult[methodsTree.Children.Count];

                int i = 0;
                foreach (TreeNode<MethodTraceService> node in methodsTree.Children)
                {
                    _methodsInfo[i++] = new MethodTraceResult(node.Data.Name, node.Data.ClassName, node.Data.Time, node);
                }

                MethodsInfo = new ReadOnlyCollection<MethodTraceResult>(_methodsInfo);
            }
            else
            {
                _methodsInfo = new MethodTraceResult[0];
                MethodsInfo = new ReadOnlyCollection<MethodTraceResult>(_methodsInfo);
            }    
        }

        IEnumerator<MethodTraceResult> IEnumerable<MethodTraceResult>.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator<MethodTraceResult> actualImplementation()
            {
                foreach (MethodTraceResult methodTraceResult in MethodsInfo)
                    yield return methodTraceResult;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return actualImplementation();

            IEnumerator actualImplementation()
            {
                foreach (MethodTraceResult methodTraceResult in MethodsInfo)
                    yield return methodTraceResult;
            }
        }
    }
}
