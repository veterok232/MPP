using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TracerLib.Interface;

namespace UnitTests.TestClasses
{
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
            Thread.Sleep(300);
            _tracer.StopTrace();
        }

        public void SuperInnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(150);
            _tracer.StopTrace();
        }
    }
}
