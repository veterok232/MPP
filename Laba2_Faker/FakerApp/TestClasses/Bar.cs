using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerApp.TestClasses
{
    public class Bar
    {
        public uint UIntA;
        public char CharA;

        public bool BoolA { get; set; }

        public override string ToString()
        {
            string result = "-----Bar-----\n" +
                "UIntA : " + UIntA.ToString() + "\n" +
                "CharA : " + CharA + "\n" +
                "BoolA : " + BoolA.ToString() + "\n";

            return result;
        }
    }
}
