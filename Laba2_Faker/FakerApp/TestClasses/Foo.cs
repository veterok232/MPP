using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerApp.TestClasses
{
    public class Foo
    {
        private string _strB;

        public decimal DecimalA;
        public double DoubleA;
        public float FloatA;
        
        public int IntA { get; private set; }
        public string StrA { get; set; }
        public Bar Bar { get; private set; }

        public List<double> DoubleList;

        public Foo(int intA)
        {
            IntA = intA;
        }

        public Foo(int intA, string strB, Bar bar)
        {
            IntA = intA;
            _strB = strB;
            Bar = bar;
        }

        public string GetStrB()
        {
            return _strB;
        }

        public override string ToString()
        {
            string result = "-----Foo-----\n" +
                "1. _strB : " + _strB + "\n" +
                "2. DecimalA : " + DecimalA.ToString() + "\n" +
                "3. DoubleA : " + DoubleA.ToString() + "\n" +
                "4. FloatA : " + FloatA + "\n" +
                "5. IntA : " + IntA.ToString() + "\n" +
                "6. StrA : " + StrA + "\n";

            result += "7. DoubleList:\n";
            int counter = 0;
            foreach (var elem in DoubleList)
            {
                result += (counter++).ToString() + ") " + elem.ToString() + "\n";
            }

            result += "8. bar: " + (Bar?.ToString() ?? "null\n");

            return result;
        }
    }
}
