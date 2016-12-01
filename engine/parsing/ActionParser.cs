/* Parses a chunk into an Action */
using System;

public class ActionParser
{
    protected string[] objectNames;

    protected string[] data; //Not used yet!!!!!!!!!!!!!!!!!!!!!!!!

    public Action Parse(Chunk c)
    {
        Action a = new Action();

        objectNames = new string[(c.header.tokens.Length - 2) / 2];
        for(int i = 3; i < c.header.tokens.Length; i += 2)
        {
            objectNames[(i - 3) / 2] = c.header.tokens[i];
        }
        return a;
    }
}
