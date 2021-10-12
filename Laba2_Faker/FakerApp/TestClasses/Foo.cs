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

        public int IntA;
        public char CharA;
        public DateTime DateTimeA;
        public Bar Bar;

        public float _floatA { get; private set; }
        public string StrA { get; set; }

        public List<double> DoubleList;

        public Foo(float floatA)
        {
            _floatA = floatA;
        }

        public Foo(float floatA, string strB)
        {
            _floatA = floatA;
            _strB = strB;
        }

        public override string ToString()
        {
            string result = "-----Foo-----\n" +
                "IntA : " + IntA.ToString() + "\n" +
                "CharA : " + CharA + "\n" +
                "DateTimeA : " + DateTimeA.ToString() + "\n" +
                "StrA : " + StrA + "\n" +
                "_floatA : " + _floatA.ToString() + "\n" +
                "_strB : " + _strB + "\n";

            result += "DoubleList:\n";
            int counter = 0;
            foreach (var elem in DoubleList)
            {
                result += (counter++).ToString() + ") " + elem.ToString() + "\n";
            }

            result += "bar: " + (Bar?.ToString() ?? "null\n");

            return result;
        }
    }
}
