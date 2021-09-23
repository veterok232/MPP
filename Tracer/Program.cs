using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            var foo = new Foo(tracer);
            foo.MyMethod();
            foo.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Console.WriteLine("threads: ");
            foreach (ThreadTraceResult threadTraceResult in traceResult)
            {
                Console.WriteLine($"    id: {threadTraceResult.ID}");
                Console.WriteLine($"    time: {threadTraceResult.Time}");
                Console.WriteLine($"    methods:");
                DisplayMethods(threadTraceResult.MethodsInfo, 2);
            }
        }

        static void DisplayMethods(ReadOnlyCollection<MethodTraceResult> methodsTrace, int level)
        {
            if (methodsTrace.Count == 0) return;

            foreach (MethodTraceResult methodTrace in methodsTrace)
            {
                DrawTabs(level);
                Console.WriteLine($"name: {methodTrace.Name}");
                DrawTabs(level);
                Console.WriteLine($"class: {methodTrace.ClassName}");
                DrawTabs(level);
                Console.WriteLine($"time: {methodTrace.Time}");
                DrawTabs(level);
                Console.WriteLine($"methods:");
                DisplayMethods(methodTrace.MethodsInfo, level + 1);
            }
            
        }

        static void DrawTabs(int num)
        {
            for (int i = 0; i < num; i++)
                Console.Write("    ");
        }
    }
}
