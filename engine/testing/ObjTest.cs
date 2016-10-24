using System;

public class MainClass
{
    static public void Main()
    {
        TestRunner t = new TestRunner();
        IObj a1 = new Obj("a");
        IObj a2 = new Obj("a");
        IObj a3 = new Obj("a");
        IObj b1 = new Obj("b");
        IObj b2 = new Obj("b");

        t.Compare("a", a1.GetType());
        t.Compare("b", b1.GetType());

        a1.PutIn(a2);
        b2.TakeIn(b1);

        t.Compare(true, a1.In(a2));
        t.Compare(false, a2.In(a1));
        t.Compare(true, a2.Contains(a1));
        t.Compare(false, a1.Contains(a2));
        t.Compare(true, b1.In(b2));
        t.Compare(false, b2.In(b1));
        t.Compare(true, b2.Contains(b1));
        t.Compare(false, b1.Contains(b2));

        a1.PutIn(a3);

        t.Compare(true, a1.In(a3));
        t.Compare(true, a3.Contains(a1));
        t.Compare(false, a1.In(a2));
        t.Compare(false, a2.Contains(a1));

        a2.PutIn(a3);

        t.Compare(true, a1.In(a3));
        t.Compare(true, a3.Contains(a1));
        t.Compare(true, a2.In(a3));
        t.Compare(true, a3.Contains(a2));
        t.Compare(false, a1.In(a2));
        t.Compare(false, a2.In(a1));

        a3.ThrowOut(a1);

        t.Compare(false, a1.In(a3));
        t.Compare(false, a3.Contains(a1));
        t.Compare(true, a2.In(a3));
        t.Compare(true, a3.Contains(a2));
        t.Compare(false, a1.In(a2));
        t.Compare(false, a2.In(a1));
    }
}
