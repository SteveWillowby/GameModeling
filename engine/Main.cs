using System;

public class MainClass
{
    public static Func<int> test1(int a)
    {
        Func<int> ret = () => a;
        a = a * 2;
        return ret;
    }

    static public void Main()
    {
        Object o = new Object("pizza");
        Console.WriteLine ("Pizza for lunch. Pizza for dinner.");
        Console.WriteLine (o.type);
        Func<int> returnsNumber = test1(3);
        Console.WriteLine ("" + test1(5)());
        Console.WriteLine ("" + returnsNumber());
    }
}
