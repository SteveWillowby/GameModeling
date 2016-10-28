using System;

public class MainClass
{
    public static TestRunner t = new TestRunner();

    public static void InCheck(IObj a, IObj b, bool yes)
    {
        t.Compare(yes, a.In(b));
        t.Compare(yes, b.Contains(a));
    }

    static public void Main()
    {
        IObj a1 = new Obj("a");
        IObj a2 = new Obj("a");
        IObj a3 = new Obj("a");
        IObj b1 = new Obj("b");
        IObj b2 = new Obj("b");

        t.Compare("a", a1.GetType());
        t.Compare("b", b1.GetType());

        a1.PutIn(a2);
        b2.TakeIn(b1);

        InCheck(a1, a2, true);
        InCheck(a2, a1, false);
        InCheck(b1, b2, true);
        InCheck(b2, b1, false);

        a1.PutIn(a3);

        InCheck(a1, a3, true);
        InCheck(a1, a2, false);

        a2.PutIn(a3);

        InCheck(a1, a3, true);
        InCheck(a2, a3, true);
        InCheck(a1, a2, false);
        InCheck(a2, a1, false);

        a3.ThrowOut(a1);

        InCheck(a1, a3, false);
        InCheck(a2, a3, true);
        InCheck(a1, a2, false);
        InCheck(a2, a1, false);

        a1.TakeIn(a3);

        InCheck(a3, a1, true);
        InCheck(a1, a3, false);
        InCheck(a2, a3, true);
        InCheck(a1, a2, false);
        InCheck(a2, a1, false);
    }
}
