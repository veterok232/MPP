using System.Threading;
using NUnit.Framework;
using TracerLib.Interface;
using TracerLib.Main;
using TracerLib.Model;
using UnitTests.TestClasses;

namespace UnitTests
{
    [TestFixture]
    public class TestsMain
    {
        private ITracer tracer;

        private Foo foo;

        private Bar bar;

        [SetUp]
        public void Setup()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
            bar = new Bar(tracer);
        }

        [Test]
        public void OneThreadTest_1Method()
        {
            foo.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.NotNull(traceResult.ThreadsInfo[0]);
            Assert.AreEqual(1, traceResult.ThreadsInfo.Count);

            Assert.AreEqual("MyMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("Foo", traceResult.ThreadsInfo[0].MethodsInfo[0].ClassName);

            Assert.AreEqual("InnerMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("Bar", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].ClassName);

            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("Bar", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].MethodsInfo[0].ClassName);
            
            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.ThreadsInfo[0].Time.Substring(0, traceResult.ThreadsInfo[0].Time.Length - 2)), 450);
        }

        [Test]
        public void OneThreadTest_2Method()
        {
            foo.MyMethod();
            bar.SuperInnerMethod();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[0].MethodsInfo[1].Name);
            Assert.AreEqual("Bar", traceResult.ThreadsInfo[0].MethodsInfo[1].ClassName);
            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.ThreadsInfo[0].Time.Substring(0, traceResult.ThreadsInfo[0].MethodsInfo[1].Time.Length - 2)), 150);
        }

        [Test]
        public void ThreeThreadsTest()
        {
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

            Assert.NotNull(traceResult.ThreadsInfo[0]);
            Assert.NotNull(traceResult.ThreadsInfo[1]);

            Assert.AreEqual(3, traceResult.ThreadsInfo.Count);

            Assert.AreEqual(3, traceResult.ThreadsInfo[0].MethodsInfo.Count);
            Assert.AreEqual(1, traceResult.ThreadsInfo[1].MethodsInfo.Count);
            Assert.AreEqual(1, traceResult.ThreadsInfo[2].MethodsInfo.Count);

            Assert.AreEqual("MyMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("InnerMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[0].MethodsInfo[0].MethodsInfo[0].MethodsInfo[0].Name);

            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.ThreadsInfo[0].Time.Substring(0, traceResult.ThreadsInfo[0].Time.Length - 2)), 1200);

            Assert.AreEqual("MyMethod", traceResult.ThreadsInfo[1].MethodsInfo[0].Name);
            Assert.AreEqual("InnerMethod", traceResult.ThreadsInfo[1].MethodsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("Bar", traceResult.ThreadsInfo[1].MethodsInfo[0].MethodsInfo[0].ClassName);
            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[1].MethodsInfo[0].MethodsInfo[0].MethodsInfo[0].Name);

            Assert.AreEqual("MyMethod3", traceResult.ThreadsInfo[2].MethodsInfo[0].Name);
            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[2].MethodsInfo[0].MethodsInfo[0].Name);
            Assert.AreEqual("SuperInnerMethod", traceResult.ThreadsInfo[2].MethodsInfo[0].MethodsInfo[1].Name);
            
            Assert.GreaterOrEqual(System.Convert.ToInt32(
                traceResult.ThreadsInfo[2].Time.Substring(0, traceResult.ThreadsInfo[2].Time.Length - 2)), 300);
        }
    }
}