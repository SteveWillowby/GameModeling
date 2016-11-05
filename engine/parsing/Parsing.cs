using System;

public class Parsing
{
    static public void Main()
    {
        Token t = new Token("value", Token.Type.Other);
        Token t2 = new Token("secondValue", Token.Type.Reserved);
        Console.WriteLine("Parsing for lunch. Parsing for dinner.");
        Console.WriteLine(t);
        Console.WriteLine(t2);
    }
}
