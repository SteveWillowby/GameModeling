/* Represents a single word in a text line */
using System;

public class Token
{
    public enum Type
    {
          Other
        , Reserved
        , Object
        , Conditional
        , Type
    };

    protected Type type;
    protected string val;

    public Token(string v, Type t)
    {
        if(v.IndexOf(' ') != -1)
        {
            throw new Exception("Cannot have a token with a ' ' in its value.");
        }
        type = t;
        val = v;
    }

    public override string ToString() 
    {
        return type + ": " + val;
    }
}
