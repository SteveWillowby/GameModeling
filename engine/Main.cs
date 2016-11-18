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
        int passIn = 3;
        Func<int> returnsNumber = test1(passIn);
        passIn = 8;

        Console.WriteLine ("" + test1(5)()); //prints out 10
        Console.WriteLine ("" + returnsNumber()); //prints out 6
    }
}
