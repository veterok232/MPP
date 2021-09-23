using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Collections.ObjectModel;

namespace Tracer
{
    public class TraceResult : IEnumerable<ThreadTraceResult>
    {
        public readonly ReadOnlyCollection<ThreadTraceResult> ThreadsInfo;

        public TraceResult(ConcurrentDictionary<int, ThreadTraceService> traceResultInfo)
        {
            ThreadTraceResult[] _threadsInfo;

            foreach (KeyValuePair<int, ThreadTraceService> pair in traceResultInfo)
            {
                _threadsInfo = new ThreadTraceResult[traceResultInfo.Count];
                _threadsInfo[pair.Key - 1] = new ThreadTraceResult(pair.Value.ID, pair.Value.Time, pair.Value.MethodsTree);
                ThreadsInfo = new ReadOnlyCollection<ThreadTraceResult>(_threadsInfo);
            }
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

    public class ThreadTraceResult : IEnumerable<MethodTraceResult>
    {
        public readonly string ID;
        public readonly string Time;
        //public readonly MethodTraceResult[] MethodsInfo;
        public readonly ReadOnlyCollection<MethodTraceResult> MethodsInfo;

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

    public class MethodTraceResult : IEnumerable<MethodTraceResult>
    {
        public readonly string Name;
        public readonly string ClassName;
        public readonly string Time;
        //public readonly MethodTraceResult[] MethodsInfo;
        public readonly ReadOnlyCollection<MethodTraceResult> MethodsInfo;

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
