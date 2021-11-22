using System;
using System.Collections.Generic;

public class Foo
{
    public IEnumerable<int> Intf { get; private set; }

    public Foo(IDisposable a, ICloneable b, int c, string d) { }

    public int TestFunction1(int e, int f)
    {
        return 0;
    }

    public void TestFunction2(){}
}

public class Baz
{
    public IEnumerable<string> Intf { get; private set; }

    public void TestFunction1() { }

    public void TestFunction2() { }
}
