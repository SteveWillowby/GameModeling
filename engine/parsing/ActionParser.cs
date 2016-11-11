/* Parses a chunk into an Action */
using System;

public class ActionParser
{
    protected string[] objectNames;
    protected string[] data;

    protected int tokenNum;

    public Action Parse(Chunk c)
    {
        Action a = new Action();

        objectNames = new string[(c.header.tokens.Length - 2) / 2];
        for(int i = 3; i < c.header.tokens.Length; i += 2)
        {
            objectNames[(i - 3) / 2] = c.header.tokens[i].val;
        }
        for(int i = 0; i < c.lines.Length; i++)
        {
            tokenNum = -1;
            c.lines[i].AssignTypes(Type);
        }
        return a;
    }

    protected Token.Type Type(string s)
    {
        tokenNum++;
        if(tokenNum == 0)
        {
            return Token.Type.Reserved;
        }
        if(Array.IndexOf(objectNames, s) != -1)
        {
            return Token.Type.Object;
        }
        return Token.Type.Other;
    }
}
