using System;

/* This class represents a line in a parsed file. */

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

        //Count the number of non-empty strings
        int numTokens = 0;
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i].Length > 0)
            {
                numTokens++;
            }
        }

        //Then save those in the tokens array
        int token = 0;
        tokens = new string[numTokens];
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i].Length > 0)
            {
                tokens[token] = s[i];
                token++;
            }
        }
    }
}
