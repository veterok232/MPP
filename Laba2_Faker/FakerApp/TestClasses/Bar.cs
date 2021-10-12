using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FakerApp.TestClasses
{
    public class Bar
    {
        public bool BoolA;
        public byte ByteA;
        public char CharA { get; set; }
        public C c;
        public IEnumerable collection;

        public DateTime DateTimeA { get; private set; }

        public Bar(DateTime dateTime)
        {
            DateTimeA = dateTime;
        }

        public override string ToString()
        {
            string result = "-----Bar-----\n" +
                "1. BoolA : " + BoolA.ToString() + "\n" +
                "2. ByterA : " + ByteA + "\n" +
                "3. CharA : " + CharA.ToString() + "\n" +
                "4. DateTimeA : " + DateTimeA.ToString() + "\n" +
                "5. c: " + (c?.ToString() ?? "null\n");

            return result;
        }
    }
}
