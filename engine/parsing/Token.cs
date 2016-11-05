/* Represents a single word in a text line */
using System;

public class Token
{
    public enum Type
    {
          Unset
        , Other
        , Reserved
        , Object
        , ObjectType
        , Conditional
        , Action
    };

    public Type type;
    public string val;

    public Token(string v)
    {
        if(v.IndexOf(' ') != -1)
        {
            throw new Exception("Cannot have a token with a ' ' in its value.");
        }
        type = Type.Unset;
        val = v;
    }

    public Token(string v, Type t) : this(v)
    {
        type = t;
    }

    public override string ToString() 
    {
        return type + ": " + val;
    }
}
