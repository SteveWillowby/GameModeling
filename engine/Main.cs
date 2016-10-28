using System;

public class MainClass
{
    static public void Main()
    {
        Object o = new Object("pizza");
        Console.WriteLine ("Pizza for lunch. Pizza for dinner.");
        Console.WriteLine (o.type);
    }
}
