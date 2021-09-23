using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace Tracer
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public void TestMethod1()
        {
            Console.WriteLine("TestMethod1");
            TestMethod2();
        }

        public void TestMethod2()
        {
            Console.WriteLine("TestMethod2");
            Console.WriteLine();
        }

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            _bar.InnerMethod();
            _tracer.StopTrace();
        }

        public void MyMethod2()
        {
            _tracer.StartTrace();
            _bar.SuperInnerMethod();
            _tracer.StopTrace();
        }

        public void MyMethod3()
        {
            _tracer.StartTrace();

            //Thread myThread = new Thread(new ThreadStart(_bar.SuperInnerMethod));    !!!!!!!!!!!!!
            //myThread.Start();                                                        !!!!!!!!!!!!!

            _bar.SuperInnerMethod();
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        public Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            SuperInnerMethod();
            int a = 0;
            for (int i = 0; i < 10000000; i++)
            {
                a++;
            }
            _tracer.StopTrace();
        }

        public void SuperInnerMethod()
        {
            _tracer.StartTrace();
            int a = 0;
            for (int i = 0; i < 50000000; i++)
            {
                a++;
            }
            _tracer.StopTrace();
        }
    }
}
