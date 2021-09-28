using System;
using System.Threading;
using TracerLib.Main;
using TracerLib.Model;
using ResultsWriter.Service;
using ResultsWriter.Main;
using ResultsWriter.TestClasses;

namespace ResultsWriter
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

            Thread myThread2 = new Thread(new ThreadStart(foo.MyMethod3));
            myThread2.Start();
            foo.MyMethod();

            myThread.Join();
            myThread2.Join();

            foo.MyMethod2();

            TraceResult traceResult = tracer.GetTraceResult();

            SerializedTraceResult serializedTraceResult = TraceResultSerializator.SerializeJson(traceResult);
            Writer.WriteToConsole(serializedTraceResult);
            Writer.WriteToFile(serializedTraceResult, "D:/");

            SerializedTraceResult serializedTraceResultXml = TraceResultSerializator.SerializeXml(traceResult);
            Writer.WriteToConsole(serializedTraceResultXml);
            Writer.WriteToFile(serializedTraceResultXml, "D:/");
        }
    }
}
