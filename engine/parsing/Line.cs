/* This class represents a line in a parsed file */
using System;

public class Line
{
    public Token[] tokens;
    public int indent;

    protected static Char[] delimiters = {' '};

    public Line(string l)
    {
        //The following two lines remove initial spaces, saving the number in indent
        for(indent = 0; indent < l.Length && l[indent] == ' '; indent++) {}
        l = l.Substring(indent);

        string[] s = l.Split(delimiters);
        tokens = new Token[s.Length];
        for(int i = 0; i < s.Length; i++)
        {
            tokens[i] = new Token(s[i]);
        }
    }

    public void AssignTypes(Func<string, Token.Type> f)
    {
        for(int i = 0; i < tokens.Length; i++)
        {
            tokens[i].type = f(tokens[i].val);
        }
    }
}
