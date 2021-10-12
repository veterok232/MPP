using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerApp.TestClasses
{
    public class C
    {
        public Foo foo { get; set; }
        public Bar bar { get; set; }

        public string strField { get; private set; }

        public override string ToString()
        {
            string result = "-----C-----\n" +
                "strField : " + strField + "\n" +
                "foo: " + (foo?.ToString() ?? "null\n") +
                "bar: " + (bar?.ToString() ?? "null\n");

            return result;
        }
    }
}
