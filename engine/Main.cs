using System;

public class MainClass
{
    static public void Main()
    {
        IObj o = new Obj("pizza");
        Console.WriteLine ("Pizza for lunch. Pizza for dinner.");
        Console.WriteLine (o.GetType());
    }
}
