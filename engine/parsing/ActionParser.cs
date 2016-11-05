/*  */
using System;

public class ActionParser
{
    protected string[] objectNames;
    protected string[] datur;

    public void Parse(Chunk c)
    {
        objectNames = new string[(c.header.tokens.Length - 2) / 2];
        for(int i = 2; i < c.header.tokens.Length; i += 2)
        {
            objectNames[(i - 2) / 2] = c.header.tokens[i].val;
        }
        for(int i = 0; i < c.lines.Length; i++)
        {
            c.lines[i].AssignTypes(Type);
        }
    }

    protected Token.Type Type(string s)
    {
        return Token.Type.Other;
    }
}
