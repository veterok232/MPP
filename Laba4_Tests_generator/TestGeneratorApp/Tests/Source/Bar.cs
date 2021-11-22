using System.Collections.Generic;

public class Bar
{
    public string s;
    public double d;

    public IEnumerable<int> Intf { get; private set; }

    public Bar(IEnumerable<int> a) { }
    
    public Bar(string s, double d)
    {
        this.s = s;
        this.d = d;
    }
    
    public void TestFunction1(int b, string c){}

    public void TestFunction2(){}
}
