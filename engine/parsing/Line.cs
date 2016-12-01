/* This class represents a line in a parsed file */
using System;

public class Line
{
    public string[] tokens;
    public int indent;

    protected static Char[] delimiters = {' '};

    public Line(string l)
    {
        //The following two lines remove initial spaces, saving the number in indent
        for(indent = 0; indent < l.Length && l[indent] == ' '; indent++) {}
        l = l.Substring(indent);

        string[] s = l.Split(delimiters);

        //This loop is for removing whitespace
        int numTokens = 0;
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i].Length > 0)
            {
                numTokens++;
            }
        }

        tokens = new string[numTokens];
        for(int i = 0; i < numTokens; i++)
        {
            if(s[i].Length > 0)
            {
                tokens[i] = s[i];
            }
        }
    }
}
