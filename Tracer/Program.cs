using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            
            Thread myThread = new Thread(new ThreadStart(foo.MyMethod));
            myThread.Start();

            foo.MyMethod();

            Thread myThread2 = new Thread(new ThreadStart(foo.MyMethod2));
            myThread2.Start();
            foo.MyMethod();
            
            myThread.Join();
            myThread2.Join();

            foo.MyMethod3();

            TraceResult traceResult = tracer.GetTraceResult();

            SerializedTraceResult serializedTraceResult = TraceResultSerializator.SerializeJson(traceResult);
            ResultsWriter.WriteToConsole(serializedTraceResult);
            ResultsWriter.WriteToFile(serializedTraceResult, "D:/");

            SerializedTraceResult serializedTraceResultXml = TraceResultSerializator.SerializeXml(traceResult);
            ResultsWriter.WriteToConsole(serializedTraceResultXml);
            ResultsWriter.WriteToFile(serializedTraceResultXml, "D:/");
        }
    }
}
