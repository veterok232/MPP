using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerApp.TestClasses
{
    public class C
    {
        private ushort _ushortA;

        public sbyte SByteA;
        public short ShortA;

        public Foo Foo { get; set; }
        public Bar Bar { get; set; }

        public uint UIntA { get; private set; }
        public ulong ULongA { get; private set; }

        private C(ushort ushortA)
        {
            _ushortA = ushortA;
        }

        public C(Foo _foo, uint uIntA)
        {
            this.Foo = _foo;
            UIntA = uIntA;
        }

        public C(Foo _foo, Bar _bar, uint uIntA, ulong uLongA, ushort uShort)
        {
            _ushortA = uShort;
            Foo = _foo;
            Bar = _bar;
            UIntA = uIntA;
            ULongA = uLongA;
        } 

        public ushort GetUShortA()
        {
            return _ushortA;
        }

        public override string ToString()
        {
            string result = "-----C-----\n" +
                "1. _ushortA : " + _ushortA.ToString() + "\n" +
                "2. SByteA : " + SByteA.ToString() + "\n" +
                "3. ShortA : " + ShortA.ToString() + "\n" +
                "4. UIntA : " + UIntA.ToString() + "\n" +
                "5. ULongA : " + ULongA.ToString() + "\n" +
                "6. foo: " + (Foo?.ToString() ?? "null\n") +
                "7. bar: " + (Bar?.ToString() ?? "null\n");

            return result;
        }
    }
}
