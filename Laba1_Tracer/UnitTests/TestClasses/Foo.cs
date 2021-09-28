using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TracerLib.Interface;

namespace UnitTests.TestClasses
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

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
            Thread.Sleep(200);
            _bar.SuperInnerMethod();
            _tracer.StopTrace();
        }

        public void MyMethod3()
        {
            _tracer.StartTrace();
            _bar.SuperInnerMethod();
            _bar.SuperInnerMethod();
            _tracer.StopTrace();
        }
    }
}
