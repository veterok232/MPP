using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Callstack
    {
        private static Callstack _singleton;
        private Stack<string> _callstack;

        private Callstack()
        {
            _callstack = new Stack<string>();
        }

        public static Callstack Get()
        {
            if (_singleton == null) _singleton = new Callstack();
            return _singleton;
        }
        public static void Push(string call)
        {
            Get()._callstack.Push(call);
        }
        public List<string> ToList()
        {
            return new List<string>(_callstack);
        }
    }
}
